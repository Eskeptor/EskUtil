// ======================================================================================================
// File Name        : EskTimer.cs
// Project          : CSUtil
// Last Update      : 2026.04.21 - yc.jeon (Eskeptor)
// ======================================================================================================

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;

namespace Esk.GearForge.CSUtil
{
    /// <summary>
    /// Custom 사용할 정밀 타이머 (Win32 API 사용) 
    /// </summary>
    public class EskTimer : IDisposable
    {
        /// <summary>
        /// 타이머의 주기 (msec)
        /// </summary>
        public int Interval
        {
            get => _interval;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                if (_timerID == 0)
                {
                    _interval = value;
                    if (Resolution >= _interval)
                    {
                        Resolution = _interval / 10 > 0 ? _interval / 10 : _interval / 5;
                    }
                }
                else
                {
                    Stop();
                    _interval = value;
                    if (Resolution >= _interval)
                    {
                        Resolution = _interval / 10 > 0 ? _interval / 10 : _interval / 5;
                    }
                    Start();
                }
            }
        }
        private int _interval;
        /// <summary>
        /// 타이머의 해상도 (msec) <br/>
        /// (항상 Interval 보다 작아야함)
        /// </summary>
        public int Resolution
        {
            get => _resolution;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                if (_timerID == 0)
                {
                    if (value >= Interval)
                    {
                        _resolution = Math.Max(value / 10, value / 5);
                    }
                    else
                    {
                        _resolution = value;
                    }
                }
                else
                {
                    Stop();
                    if (value >= Interval)
                    {
                        _resolution = Math.Max(value / 10, value / 5);
                    }
                    else
                    {
                        _resolution = value;
                    }
                    Start();
                }
            }
        }
        private int _resolution;
        /// <summary>
        /// 현재 타이머가 실행중인지 유무
        /// </summary>
        public bool IsRunning { get => _timerID != 0; }
        /// <summary>
        /// 타이머가 실행될 때 주기적으로 호출할 이벤트
        /// </summary>
        public event EventHandler<ElapseEventArgs> Elapsed;
        /// <summary>
        /// Timer가 Start한 시간 <br/>
        /// (Start하지 않았거나 Stop한 경우 DateTime.MinValue)
        /// </summary>
        public DateTime StartTime { get => _startTime; }
        private DateTime _startTime = DateTime.MinValue;

        /// <summary>
        /// 생성된 타이머의 ID
        /// </summary>
        private uint _timerID;
        /// <summary>
        /// Dispose 유무
        /// </summary>
        private bool _disposed;
        /// <summary>
        /// Native 함수인 TimeSetEvent에 연결할 함수 포인터를 대신할 delegate
        /// </summary>
        private readonly WinAPI.TimerCallback Callback;
        /// <summary>
        /// 타이머 콜백에서 발생하는 이벤트(Elapsed)를 타이머가 생성된 스레드의 컨텍스트(예: UI 스레드)에서 안전하게 실행하기 위한 SynchronizationContext. <br/>
        /// (동기화 컨텍스트가 없으면 기본 SynchronizationContext를 사용)
        /// </summary>
        private SynchronizationContext SyncContext { get; }
        /// <summary>
        /// Timer Lock
        /// </summary>
        private object Locker { get; } = new object();
        /// <summary>
        /// UI에서 사용하는지 유무
        /// </summary>
        private bool UseUIContext { get; }
        /// <summary>
        /// Elapse 이벤트가 발생했을 때 전달할 데이터
        /// </summary>
        private object ElapseData { get; }
        /// <summary>
        /// 콜백 재진입 방지용 변수 (타이머 콜백이 실행되는 동안 다시 콜백이 실행되지 않도록 하기 위한 변수) <br/>
        /// </summary>
        private int _syncPoint;

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="useUIConext">UI에서 사용하는지 유무</param>
        /// <param name="elapseData">Elapse 이벤트가 발생했을 때 전달할 데이터</param>
        public EskTimer(bool useUIConext, object elapseData = null)
            : this(10, 2, useUIConext, elapseData)
        {

        }
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="interval">타이머 주기 (msec)</param>
        /// <param name="useUIConext">UI에서 사용하는지 유무</param>
        /// <param name="elapseData">Elapse 이벤트가 발생했을 때 전달할 데이터</param>
        public EskTimer(int interval, bool useUIConext, object elapseData = null)
            : this(interval, interval / 10, useUIConext, elapseData)
        {

        }
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="interval">타이머의 주기 (msec)</param>
        /// <param name="resolution">타이머의 해상도 (msec) <br/>(항상 <paramref name="interval"/>보다 작아야 함)</param>
        /// <param name="useUIConext">UI에서 사용하는지 유무</param>
        /// <param name="elapseData">Elapse 이벤트가 발생했을 때 전달할 데이터</param>
        public EskTimer(int interval, int resolution, bool useUIConext, object elapseData = null)
        {
            Resolution = resolution;
            Interval = interval;
            UseUIContext = useUIConext;
            ElapseData = elapseData;
            Callback = new WinAPI.TimerCallback(TimerCallbackMethod);
            SyncContext = SynchronizationContext.Current;
        }
        /// <summary>
        /// 소멸자
        /// </summary>
        ~EskTimer()
        {
            Dispose(false);
        }
        /// <summary>
        /// Override - IDisposable
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 타이머를 시작하는 함수
        /// </summary>
        /// <exception cref="Win32Exception">타이머 생성에 실패했을 때 (WinAPI 함수 실행 실패)</exception>
        /// <returns>
        /// true: 타이머 실행 성공 <br/>
        /// false: Dispose 되었거나 이미 타이머가 실행중일 때
        /// </returns>
        public bool Start()
        {
            lock (Locker)
            {
                if (_disposed ||
                    IsRunning)
                {
                    return false;
                }

                uint userCtx = 0;
                _timerID = WinAPI.TimeSetEvent((uint)Interval, (uint)Resolution, Callback, ref userCtx, WinAPI.TIME_PERIODIC | WinAPI.TIME_CALLBACK_FUNCTION);
                if (_timerID == 0)
                {
                    int error = Marshal.GetLastWin32Error();
                    throw new Win32Exception(error);
                }
                _startTime = DateTime.Now;
            }
            return true;
        }
        /// <summary>
        /// 타이머를 정지하는 함수
        /// </summary>
        public void Stop()
        {
            lock (Locker)
            {
                if (!_disposed &&
                    IsRunning)
                {
                    InternalStop();
                }
            }
        }
        /// <summary>
        /// 내부 함수 - 타이머를 정지하는 함수
        /// </summary>
        private void InternalStop()
        {
            WinAPI.TimeKillEvent(_timerID);
            _timerID = 0;
            _startTime = DateTime.MinValue;
        }
        /// <summary>
        /// Dispose 함수
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                if (IsRunning)
                {
                    InternalStop();
                }

                Elapsed = null;
                _disposed = true;
            }
        }
        /// <summary>
        /// Native 함수인 TimeSetEvent 함수의 Callback 함수
        /// </summary>
        /// <param name="id"></param>
        /// <param name="msg"></param>
        /// <param name="userCtx"></param>
        /// <param name="rsv1"></param>
        /// <param name="rsv2"></param>
        private void TimerCallbackMethod(uint id, uint msg, ref uint userCtx, uint rsv1, uint rsv2)
        {
            // 이전 작업이 안 끝났으면 이번 틱은 무시 (Drop frame 전략)
            if (Interlocked.CompareExchange(ref _syncPoint, 1, 0) != 0)
            {
                return;
            }

            try
            {
                EventHandler<ElapseEventArgs> handler = Elapsed;
                if (handler == null)
                {
                    return;
                }

                SynchronizationContext ctx = SyncContext;
                if (UseUIContext)
                {
                    if (ctx == null ||
                        ctx.GetType() == typeof(SynchronizationContext))
                    {
                        handler.Invoke(this, new ElapseEventArgs() { Data = ElapseData });
                    }
                    else
                    {
                        ctx.Post(_ =>
                        {
                            try { handler.Invoke(this, new ElapseEventArgs() { Data = ElapseData }); }
                            catch { }
                        }, null);
                    }
                }
                else
                {
                    handler.Invoke(this, new ElapseEventArgs() { Data = ElapseData });
                }
            }
            finally
            {
                Interlocked.Exchange(ref _syncPoint, 0);
            }
        }
    }

    public class ElapseEventArgs : EventArgs
    {
        /// <summary>
        /// 데이터
        /// </summary>
        public object Data { get; set; }
    }
}
