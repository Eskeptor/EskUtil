// ======================================================================================================
// File Name        : EskTask.cs
// Project          : CSUtil
// Last Update      : 2024.09.16 - yc.jeon
// ======================================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSUtil
{
    /// <summary>
    /// Custom Task (Task와 CancellationTokenSource를 같이 관리)
    /// </summary>
    public class EskTask : IDisposable
    {
        /// <summary>
        /// Task 객체
        /// </summary>
        public Task RunTask { get; }
        /// <summary>
        /// Task Cancel 유무
        /// </summary>
        public bool IsCancel { get { return _cancelToken.Token.IsCancellationRequested; } }
        /// <summary>
        /// Task 이름
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// 객체 Dispose 유무
        /// </summary>
        protected bool IsDisposed { get; private set; }
        /// <summary>
        /// Task의 CancellationTokenSource
        /// </summary>
        private CancellationTokenSource _cancelToken;
        /// <summary>
        /// Task의 Token을 외부에서 받아오지 않고 내부에서 생성했는지 유무
        /// </summary>
        private bool _isTokenInternalCreated;
        /// <summary>
        /// Task의 Start 함수가 한번이라도 호출되었는지 유무
        /// </summary>
        private bool _isStarted;

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="name">Task의 이름</param>
        /// <param name="action">Task 수행 Action</param>
        /// <param name="isStart">생성과 동시에 Start 할 지 유무</param>
        public EskTask(string name, Action action, bool isStart = false)
        {
            Name = name;
            _cancelToken = new CancellationTokenSource();
            _isTokenInternalCreated = true;
            _isStarted = isStart;
            RunTask = new Task(action);

            if (isStart)
            {
                RunTask.Start();
            }
        }
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="name">Task의 이름</param>
        /// <param name="action">Task 수행 Action</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <param name="isStart">생성과 동시에 Start 할 지 유무</param>
        public EskTask(string name, Action action, CancellationTokenSource cancellationToken, bool isStart = false)
        {
            Name = name;
            _cancelToken = cancellationToken;
            _isStarted = isStart;
            RunTask = new Task(action);

            if (isStart)
            {
                RunTask.Start();
            }
        }
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="name">Task의 이름</param>
        /// <param name="action">Task 수행 Action</param>
        /// <param name="options">Task 생성 옵션값</param>
        /// <param name="isStart">생성과 동시에 Start 할 지 유무</param>
        public EskTask(string name, Action action, TaskCreationOptions options, bool isStart = false)
        {
            Name = name;
            _cancelToken = new CancellationTokenSource();
            _isTokenInternalCreated = true;
            _isStarted = isStart;
            RunTask = new Task(action, options);

            if (isStart)
            {
                RunTask.Start();
            }
        }
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="name">Task의 이름</param>
        /// <param name="action">Task 수행 Action</param>
        /// <param name="options">Task 생성 옵션값</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <param name="isStart">생성과 동시에 Start 할 지 유무</param>
        public EskTask(string name, Action action, TaskCreationOptions options, CancellationTokenSource cancellationToken, bool isStart = false)
        {
            Name = name;
            _cancelToken = cancellationToken;
            _isStarted = isStart;
            RunTask = new Task(action, options);

            if (isStart)
            {
                RunTask.Start();
            }
        }
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="name">Task의 이름</param>
        /// <param name="action">Task 수행 Action (이때 object 타입은 KairosTaskData)</param>
        /// <param name="state"><paramref name="action"/>에 넘겨줄 데이터 (이 데이터는 KairosTaskData.Data에 들어감)</param>
        /// <param name="isStart">생성과 동시에 Start 할 지 유무</param>
        public EskTask(string name, Action<object?> action, object? state, bool isStart = false)
        {
            Name = name;
            _cancelToken = new CancellationTokenSource();
            _isTokenInternalCreated = true;
            _isStarted = isStart;
            RunTask = new Task(action, new EskTaskData(state, _cancelToken));

            if (isStart)
            {
                RunTask.Start();
            }
        }
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="name">Task의 이름</param>
        /// <param name="action">Task 수행 Action (이때 object 타입은 KairosTaskData)</param>
        /// <param name="state"><paramref name="action"/>에 넘겨줄 데이터 (이 데이터는 KairosTaskData.Data에 들어감)</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <param name="isStart">생성과 동시에 Start 할 지 유무</param>
        public EskTask(string name, Action<object?> action, object? state, CancellationTokenSource cancellationToken, bool isStart = false)
        {
            Name = name;
            _cancelToken = cancellationToken;
            _isStarted = isStart;
            RunTask = new Task(action, new EskTaskData(state, _cancelToken));

            if (isStart)
            {
                RunTask.Start();
            }
        }
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="name">Task의 이름</param>
        /// <param name="action">Task 수행 Action (이때 object 타입은 KairosTaskData)</param>
        /// <param name="state"><paramref name="action"/>에 넘겨줄 데이터 (이 데이터는 KairosTaskData.Data에 들어감)</param>
        /// <param name="options">Task 생성 옵션값</param>
        /// <param name="isStart">생성과 동시에 Start 할 지 유무</param>
        public EskTask(string name, Action<object?> action, object? state, TaskCreationOptions options, bool isStart = false)
        {
            Name = name;
            _cancelToken = new CancellationTokenSource();
            _isTokenInternalCreated = true;
            _isStarted = isStart;
            RunTask = new Task(action, new EskTaskData(state, _cancelToken), options);

            if (isStart)
            {
                RunTask.Start();
            }
        }
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="name">Task의 이름</param>
        /// <param name="action">Task 수행 Action (이때 object 타입은 KairosTaskData)</param>
        /// <param name="state"><paramref name="action"/>에 넘겨줄 데이터 (이 데이터는 KairosTaskData.Data에 들어감)</param>
        /// <param name="options">Task 생성 옵션값</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <param name="isStart">생성과 동시에 Start 할 지 유무</param>
        public EskTask(string name, Action<object?> action, object? state, TaskCreationOptions options, CancellationTokenSource cancellationToken, bool isStart = false)
        {
            Name = name;
            _cancelToken = cancellationToken;
            _isStarted = isStart;
            RunTask = new Task(action, new EskTaskData(state, _cancelToken), options);

            if (isStart)
            {
                RunTask.Start();
            }
        }

        /// <summary>
        /// 소멸자
        /// </summary>
        ~EskTask()
        {
            Dispose(false);
        }

        /// <summary>
        /// Dispose 함수
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose 함수
        /// </summary>
        /// <param name="disposing">Dispose 실행할지 유무</param>
        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed)
            {
                return;
            }

            if (disposing)
            {
                Stop(true);
                RunTask.Dispose();
                if (_isTokenInternalCreated)
                {
                    _cancelToken.Dispose();
                }
            }

            IsDisposed = true;
        }

        /// <summary>
        /// Task Start
        /// </summary>
        public void Start()
        {
            _isStarted = true;
            RunTask.Start();
        }

        /// <summary>
        /// Task Stop
        /// </summary>
        /// <param name="isWait">Stop 이후에 Task 수행이 끝날때 까지 기다릴지 유무</param>
        public void Stop(bool isWait = false)
        {
            if (IsDisposed)
            {
                return;
            }

            _cancelToken.Cancel();

            if (isWait &&
                _isStarted)
            {
                if (RunTask.IsCompleted)
                {
                    return;
                }

                RunTask.Wait();
            }

            _isStarted = false;
        }

        /// <summary>
        /// Task가 정지했는지(Stop) 유무
        /// </summary>
        /// <returns></returns>
        public bool IsStop()
        {
            return RunTask.IsCompleted && !_isStarted;
        }

        public override string ToString()
        {
            return $"{Name} Task ({(_isStarted || !RunTask.IsCompleted ? "Running" : "Stopped")})";
        }
    }

    /// <summary>
    /// Esk Task에서 사용하는 데이터
    /// </summary>
    public sealed class EskTaskData
    {
        /// <summary>
        /// EskTask로 넘겨줄 데이터
        /// </summary>
        public object? Data { get; }
        /// <summary>
        /// Task 종료 플래그
        /// </summary>
        public bool IsCanceled { get { return _cancelToken.Token.IsCancellationRequested; } }
        /// <summary>
        /// Task CancellationTokenSource
        /// </summary>
        private CancellationTokenSource _cancelToken;

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="data">데이터</param>
        /// <param name="cancelToken">Task CancellationTokenSource</param>
        public EskTaskData(object? data, CancellationTokenSource cancelToken)
        {
            Data = data;
            _cancelToken = cancelToken;
        }
    }
}
