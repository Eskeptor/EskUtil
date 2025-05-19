// ======================================================================================================
// File Name        : LogCleaner.cs
// Project          : CSUtil
// Last Update      : 2025.05.19 - yc.jeon
// ======================================================================================================

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CSUtil
{
    /// <summary>
    /// Log Cleaner
    /// </summary>
    public class LogCleaner : IDisposable
    {
        /// <summary>
        /// Log File을 제거했을 때 호출하는 이벤트
        /// </summary>
        public EventHandler<DeleteEventArgs> DeleteEvent { get; set; }

        /// <summary>
        /// Log Cleaner의 옵션 데이터
        /// </summary>
        public LogCleanerOption Option { get { return _option; } }
        private LogCleanerOption _option = new LogCleanerOption();

        /// <summary>
        /// Log 탐색할 대상 디렉토리
        /// </summary>
        public List<string> SearchDirectories { get; } = new List<string>();

        /// <summary>
        /// Cleaner 동작이 시작되었는지 유무
        /// </summary>
        public bool IsStart { get { return _isStart; } }
        private bool _isStart;
        /// <summary>
        /// Cleaner 동작 Task
        /// </summary>
        private EskTask _cleanTask;
        /// <summary>
        /// Log Cleaner 객체 Dispose 유무
        /// </summary>
        private bool _isDisposed;

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="option">Log Cleaner 옵션 데이터</param>
        public LogCleaner(LogCleanerOption option = null)
        {
            if (option != null)
            {
                _option = option;
            }
        }
        /// <summary>
        /// 소멸자
        /// </summary>
        ~LogCleaner()
        {
            Dispose(false);
        }
        /// <summary>
        /// Override - Dispose (IDisposable)
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
                Stop();
                _cleanTask?.Dispose();
            }

            _isDisposed = true;
        }
        /// <summary>
        /// Override - Dispose (IDisposable)
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// Log Cleaner 탐색 대상 디렉토리 추가
        /// </summary>
        /// <param name="directory">탐색 대상 디렉토리</param>
        public void AddDirectory(string directory)
        {
            if (IsStart)
            {
                return;
            }

            SearchDirectories.Add(directory);
        }
        /// <summary>
        /// Log Cleaner 탐색 대상 디렉토리 추가
        /// </summary>
        /// <param name="directories">탐색 대상 디렉토리</param>
        public void AddDirectory(IEnumerable<string> directories)
        {
            if (IsStart)
            {
                return;
            }

            SearchDirectories.AddRange(directories);
        }
        /// <summary>
        /// Log Cleaner 탐색 대상 디렉토리 초기화 (디렉토리 목록 전체 제거)
        /// </summary>
        public void ClearDirectory()
        {
            if (IsStart)
            {
                return;
            }

            SearchDirectories.Clear();
        }
        /// <summary>
        /// Log Cleaner 옵션 데이터 설정
        /// </summary>
        /// <param name="option">Log Cleaner 옵션 데이터</param>
        public void SetOption(LogCleanerOption option)
        {
            _option = option;
        }
        /// <summary>
        /// Log Cleaner 옵션 데이터 설정
        /// </summary>
        /// <param name="day">일</param>
        /// <param name="hour">시간</param>
        /// <param name="minute">분</param>
        /// <param name="isIncludeSubDirectory">하위 디렉토리 탐색 유무</param>
        /// <param name="searchDelay">Cleaner 동작 주기</param>
        public void SetOption(int day = 30, int hour = 0, int minute = 0, bool isIncludeSubDirectory = false, int searchDelay = 10000)
        {
            Option.Day = day;
            Option.Hour = hour;
            Option.Minute = minute;
            Option.IncludeSubDirectory = isIncludeSubDirectory;
            Option.SearchInterval = searchDelay;
        }
        /// <summary>
        /// Log Cleaner 시작
        /// </summary>
        /// <returns></returns>
        public bool Start()
        {
            if (_isStart)
            {
                return false;
            }

            if (_cleanTask != null)
            {
                _cleanTask.Stop(true);
                _cleanTask.Dispose();
            }

            _cleanTask = new EskTask("LogClean", CleanTask, null, true);
            _isStart = true;
            return true;
        }
        /// <summary>
        /// Log Cleaner 종료
        /// </summary>
        public void Stop()
        {
            if (_cleanTask == null)
            {
                return;
            }

            _cleanTask.Stop(true);
            _isStart = false;
        }
        /// <summary>
        /// Cleaner 동작 Task 함수
        /// </summary>
        /// <param name="state"></param>
        private void CleanTask(object state)
        {
            if (!(state is EskTaskData taskData))
            {
                return;
            }
            bool isAllExtension = Option.IsAllExtension;
            if (isAllExtension)
            {
                Option.AddExtension("*");
            }

            List<string> deletedFiles = new List<string>(64);
            TimeSpan maxAge = Option.MaxAge;
            SearchOption searchOption = Option.IncludeSubDirectory ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            Stopwatch stopwatch = new Stopwatch();
            int timeDiff;
            while (!taskData.IsCanceled)
            {
                stopwatch.Restart();

                foreach (string directory in SearchDirectories)
                {
                    if (taskData.IsCanceled)
                    {
                        return;
                    }
                    if (!Directory.Exists(directory))
                    {
                        continue;
                    }

                    foreach (string extension in Option.FileExtensions)
                    {
                        try
                        {
                            string[] files = Directory.GetFiles(directory, $"*.{extension}", searchOption);
                            foreach (string file in files)
                            {
                                if (taskData.IsCanceled)
                                {
                                    return;
                                }

                                FileInfo fileInfo = new FileInfo(file);
                                if (fileInfo.LastWriteTime > DateTime.Now.Subtract(maxAge))
                                {
                                    continue;
                                }

                                try
                                {
                                    if (IsFileLocked(fileInfo))
                                    {
                                        continue;
                                    }

                                    fileInfo.Delete();

                                    if (DeleteEvent != null)
                                    {
                                        deletedFiles.Add(file);
                                    }
                                }
                                catch { }
                            }
                        }
                        catch { }
                    }
                }
                if (deletedFiles.Count > 0)
                {
                    if (DeleteEvent != null)
                    {
                        IReadOnlyList<string> files = deletedFiles.ToList();
                        Task.Run(() => DeleteEvent(null, new DeleteEventArgs() { DeletedFiles = files }));
                    }
                    deletedFiles.Clear();
                }

                timeDiff = Math.Max(1, Convert.ToInt32(Option.SearchInterval - stopwatch.ElapsedMilliseconds));
                Thread.Sleep(timeDiff);
            }

            if (isAllExtension)
            {
                Option.RemoveExtension("*");
            }
        }
        /// <summary>
        /// 대상 파일이 잠겨있는지 확인하는 함수
        /// </summary>
        /// <param name="fileInfo">확인할 파일</param>
        /// <returns>
        /// true: 잠겨있음 <br/>
        /// false: 잠겨있지 않음
        /// </returns>
        private static bool IsFileLocked(FileInfo fileInfo)
        {
            try
            {
                using (FileStream stream = fileInfo.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Log Cleaner 옵션 데이터
    /// </summary>
    public class LogCleanerOption
    {
        /// <summary>
        /// Cleaning 기준 일자
        /// </summary>
        public int Day { get; set; } = 30;
        /// <summary>
        /// Cleaning 기준 시간
        /// </summary>
        public int Hour
        {
            get { return _hour; }
            set
            {
                if (value >= 24)
                {
                    Day += value / 24;
                    _hour = value % 24;
                }
                else if (value >= 0)
                {
                    _hour = value;
                }
                else
                {
                    _hour = 0;
                }
            }
        }
        private int _hour;
        /// <summary>
        /// Cleaning 기준 분
        /// </summary>
        public int Minute
        {
            get { return _minute; }
            set
            {
                if (value >= 60)
                {
                    Hour += value / 60;
                    _minute = value % 60;
                }
                else if (value >= 0)
                {
                    _minute = value;
                }
                else
                {
                    _minute = 0;
                }
            }
        }
        private int _minute;
        /// <summary>
        /// 하위 디렉토리 탐색 유무
        /// </summary>
        public bool IncludeSubDirectory { get; set; }
        /// <summary>
        /// Cleaner 탐색 주기
        /// </summary>
        public int SearchInterval
        {
            get { return _searchInterval; }
            set { _searchInterval = value > 0 ? value : 60000; }
        }
        private int _searchInterval = 60000;
        /// <summary>
        /// Log 파일 확장자들
        /// </summary>
        public List<string> FileExtensions { get; } = new List<string>(4);
        /// <summary>
        /// 전체 파일 확장자를 사용하는지 유무 <br/>
        /// (FileExtensions에 선언된 파일 확장자가 없으면 전체 파일 확장자를 사용하는 것으로 간주, 폴더 제외)
        /// </summary>
        [XmlIgnore]
        public bool IsAllExtension { get { return FileExtensions.Count == 0; } }
        /// <summary>
        /// Cleaning 기준 기간 (Day + Hour + Minute)
        /// </summary>
        [XmlIgnore]
        public TimeSpan MaxAge
        {
            get { return TimeSpan.FromDays(Day) + TimeSpan.FromHours(_hour) + TimeSpan.FromMinutes(_minute); }
        }
        /// <summary>
        /// Lock Object
        /// </summary>
        private object Lock { get; } = new object();

        //public LogCleanerOption(int day = 30, int hour = 0, int minute = 0, bool isIncludeSubFolder = false, int searchDelay = 10000, List<string> fileExtensions = null)
        //{
        //    Day = day;
        //    Hour = hour;
        //    Minute = minute;
        //    IncludeSubFolder = isIncludeSubFolder;
        //    SearchDelay = searchDelay;

        //    if (fileExtensions != null &&
        //        fileExtensions.Count > 0)
        //    {
        //        FileExtensions.AddRange(fileExtensions);
        //        ValidateExtension(true);
        //    }
        //}

        /// <summary>
        /// Log 파일 확장자를 추가하는 함수
        /// </summary>
        /// <param name="extension">Log 파일 확장자</param>
        public void AddExtension(string extension)
        {
            if (string.IsNullOrEmpty(extension))
            {
                return;
            }
            if (extension[0] == '.')
            {
                extension = extension.Substring(1);
            }

            lock (Lock)
            {
                FileExtensions.Add(extension);
            }
        }
        /// <summary>
        /// Log 파일 확장자를 추가하는 함수
        /// </summary>
        /// <param name="extensions">Log 파일 확장자</param>
        public void AddExtension(IEnumerable<string> extensions)
        {
            if (extensions == null ||
                !extensions.Any())
            {
                return;
            }

            lock (Lock)
            {
                FileExtensions.AddRange(extensions);
            }
            ValidateExtension();
        }
        /// <summary>
        /// Log 파일 확장자를 제거하는 함수
        /// </summary>
        /// <param name="index">제거할 파일 확장자의 인덱스</param>
        public void RemoveExtension(int index)
        {
            if (FileExtensions.Count <= index)
            {
                return;
            }

            lock (Lock)
            {
                FileExtensions.RemoveAt(index);
            }
        }
        /// <summary>
        /// Log 파일 확장자를 제거하는 함수
        /// </summary>
        /// <param name="extension">제거할 파일 확장자</param>
        public void RemoveExtension(string extension)
        {
            if (string.IsNullOrEmpty(extension))
            {
                return;
            }

            lock (Lock)
            {
                FileExtensions.Remove(extension);
            }
        }
        /// <summary>
        /// Log 파일 확장자 전체 검증
        /// </summary>
        public void ValidateExtension()
        {
            lock (Lock)
            {
                if (FileExtensions.Count == 0)
                {
                    return;
                }

                for (int i = 0; i < FileExtensions.Count; ++i)
                {
                    string extension = FileExtensions[i];
                    if (string.IsNullOrEmpty(extension))
                    {
                        continue;
                    }
                    if (extension[0] == '.')
                    {
                        extension = extension.Substring(1);
                    }

                    FileExtensions[i] = extension;
                }
            }
        }
        /// <summary>
        /// Override - Equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is LogCleanerOption option))
            {
                return false;
            }
            if (FileExtensions.Count != option.FileExtensions.Count)
            {
                return false;
            }
            for (int i = 0; i < FileExtensions.Count; ++i)
            {
                if (!FileExtensions[i].Equals(option.FileExtensions[i], StringComparison.Ordinal))
                {
                    return false;
                }
            }

            return Day == option.Day && Hour == option.Hour && Minute == option.Minute &&
                IncludeSubDirectory == option.IncludeSubDirectory && SearchInterval == option.SearchInterval;
        }
        /// <summary>
        /// Override - GetHashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Day, Hour, Minute, IncludeSubDirectory, SearchInterval, FileExtensions);
        }
        /// <summary>
        /// Log Cleaner 옵션 데이터를 Deep Copy 하는 함수
        /// </summary>
        /// <returns></returns>
        public LogCleanerOption MakeCopy()
        {
            LogCleanerOption option = new LogCleanerOption()
            {
                Day = Day,
                Hour = Hour,
                Minute = Minute,
                SearchInterval = SearchInterval,
                IncludeSubDirectory = IncludeSubDirectory,
            };
            if (FileExtensions.Count > 0)
            {
                option.AddExtension(FileExtensions);
            }
            return option;
        }
    }

    public class DeleteEventArgs : EventArgs
    {
        public IReadOnlyList<string> DeletedFiles { get; set; }
    }
}
