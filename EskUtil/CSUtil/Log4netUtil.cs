// ======================================================================================================
// File Name        : Log4netUtil.cs
// Project          : CSUtil
// Last Update      : 2024.01.27 - yc.jeon
// ======================================================================================================

using log4net.Util;
using System.Diagnostics;
using System.Reflection;

namespace CSUtil
{
    /// <summary>
    /// log4net용 유틸리티
    /// </summary>
    namespace Log4netUtil
    {
        /// <summary>
        /// log4net을 이용한 로그 매니저
        /// </summary>
        public class LogManager
        {
            /// <summary>
            /// RollingFileAppender 설정용
            /// </summary>
            public class LogRollingSettting
            {
                /// <summary>
                /// 로그 내용 추가(Append) 사용 유무
                /// </summary>
                public bool IsAppendToFile { get; set; } = false;
                /// <summary>
                /// 최대 RollBackups 크기
                /// </summary>
                public int MaxSizeRollBackups { get; set; } = 5;
                /// <summary>
                /// 최대 파일 크기
                /// </summary>
                public string MaximumFileSize { get; set; } = "100KB";
                /// <summary>
                /// Rolling Mode
                /// </summary>
                public log4net.Appender.RollingFileAppender.RollingMode RollingMode { get; set; } = log4net.Appender.RollingFileAppender.RollingMode.Composite;
            }

            /// <summary>
            /// Conversion Pattern 기본값
            /// </summary>
            //private const string DEFAULT_PATTERN_CONVERSION = "%date [%thread] %-5level %logger - %message%newline";
            private const string DEFAULT_PATTERN_CONVERSION = "%date [%-5level]%message%newline";
            /// <summary>
            /// Data Pattern 기본값
            /// </summary>
            private const string DEFAULT_PATTERN_DATE = "yyyy-MM-dd";
            /// <summary>
            /// 로그 기본 확장자
            /// </summary>
            private const string DEFAULT_EXE = ".log";

            /// <summary>
            /// 로그 폴더 경로
            /// </summary>
            public string LogFolderPath { get; private set; } = string.Empty;
            /// <summary>
            /// 로그 이름
            /// </summary>
            public string LogName { get; private set; } = string.Empty;
            /// <summary>
            /// 로그 전체 경로 (LogFolderPath + LogName)
            /// </summary>
            public string LogFullPath { get; private set; } = string.Empty;
            /// <summary>
            /// 폴더 구분자 <br/>
            /// </summary>
            /// <remarks>
            /// 예시:<br/>
            /// 폴더 구분자가 "." 일때 logName이 "ABC.EFG.H"라면 ABC 폴더를 만들고, 그 안에 EFG 폴더를 만들고, 그 안에 H 폴더를 만들고 로그를 쓴다.
            /// </remarks>
            public char FolderSeparator { get; private set; } = '.';
            /// <summary>
            /// 로그 사용 유무 (변경은 생성자에서만 가능)
            /// </summary>
            public bool IsLogUsed { get; } = true;
            /// <summary>
            /// Log4Net Logger
            /// </summary>
            private log4net.ILog _logger = null!;

            /// <summary>
            /// 생성자
            /// </summary>
            /// <param name="logFolderPath">로그 폴더 경로</param>
            /// <param name="logName">로그 이름</param>
            /// <param name="conversionPattern">Conversion Pattern (For Log4Net RollingFileAppender)</param>
            /// <param name="datePattern">Date Pattern (For Log4Net RollingFileAppender)</param>
            /// <param name="rollingSettting">RollingFileAppender 설정값</param>
            /// <param name="isLogUsed">로그 사용 유무</param>
            /// <param name="folderSeparator">폴더 구분자</param>
            /// <exception cref="ArgumentNullException"></exception>
            /// <exception cref="DirectoryNotFoundException"></exception>
            public LogManager(string logFolderPath, string logName, string conversionPattern, string datePattern, LogRollingSettting rollingSettting, bool isLogUsed, char folderSeparator = '.')
            {
                IsLogUsed = isLogUsed;
                if (!IsLogUsed)
                {
                    WriteInternalLog("Log not used.");
                    return;
                }

                if (string.IsNullOrEmpty(logFolderPath))
                {
                    WriteInternalLog("Log folder path is null or empty.");
                    throw new ArgumentNullException(nameof(logFolderPath), "Log folder path is null or empty.");
                }
                if (string.IsNullOrEmpty(logName))
                {
                    WriteInternalLog("Log name is null or empty");
                    throw new ArgumentNullException(nameof(logName), "Log name is null or empty");
                }

                if (string.IsNullOrEmpty(conversionPattern))
                {
                    conversionPattern = DEFAULT_PATTERN_CONVERSION;
                }
                if (string.IsNullOrEmpty(datePattern))
                {
                    datePattern = DEFAULT_PATTERN_DATE;
                }

                FolderSeparator = folderSeparator;
                LogFolderPath = logFolderPath;
                LogName = logName;
                LogFullPath = $"{logFolderPath}{Path.DirectorySeparatorChar}{logName}{Path.DirectorySeparatorChar}";
                if (!InitLogFolder())
                {
                    throw new DirectoryNotFoundException($"Log folder({LogFullPath}) create failed.");
                }

                bool firstCreate = false;
                log4net.Repository.ILoggerRepository? repository = null;
                try
                {
                    repository = log4net.LogManager.CreateRepository(logName);

                    log4net.Layout.PatternLayout patternLayout = new log4net.Layout.PatternLayout()
                    {
                        ConversionPattern = conversionPattern,
                    };
                    patternLayout.ActivateOptions();

                    log4net.Appender.RollingFileAppender rollingAppender = new log4net.Appender.RollingFileAppender()
                    {
                        Name = logName,
                        File = LogFullPath,
                        DatePattern = $"{datePattern}'{DEFAULT_EXE}'",
                        StaticLogFileName = false,
                        AppendToFile = rollingSettting.IsAppendToFile,
                        RollingStyle = rollingSettting.RollingMode,
                        MaxSizeRollBackups = rollingSettting.MaxSizeRollBackups,
                        MaximumFileSize = rollingSettting.MaximumFileSize,
                        Layout = patternLayout,
                        LockingModel = new log4net.Appender.RollingFileAppender.MinimalLock()
                    };
                    AsyncAppender asyncAppender = new AsyncAppender();

                    rollingAppender.ActivateOptions();
                    log4net.Config.BasicConfigurator.Configure(repository, asyncAppender, rollingAppender);
                    firstCreate = true;
                }
                catch
                {
                    //Console.WriteLine(ex.ToString());
                    repository = log4net.LogManager.GetRepository(logName);
                }

                _logger = log4net.LogManager.GetLogger(logName, logName);

                if (firstCreate)
                {
                    Info("New log start");
                }
            }

            /// <summary>
            /// 로그 파일의 이름을 변경하는 함수
            /// </summary>
            /// <param name="fileName">로그 파일의 이름</param>
            /// <param name="filePath">
            /// 변경할 로그 파일의 경로 <br/>
            /// (기본값 : 공백 = 기존 폴더 경로 유지)
            /// </param>
            public void FileNameChange(string fileName, string filePath = "")
            {
                if (!IsLogUsed)
                {
                    WriteInternalLog("Log not used.");
                    return;
                }

                if (string.IsNullOrEmpty(fileName))
                {
                    Error("Filename is null or empty.");
                    return;
                }
                if (string.IsNullOrEmpty(filePath))
                {
                    filePath = LogFullPath;
                }

                log4net.Repository.Hierarchy.Hierarchy rootHierarchy = (log4net.Repository.Hierarchy.Hierarchy)_logger.Logger.Repository;
                log4net.Appender.FileAppender fileAppender = (log4net.Appender.FileAppender)rootHierarchy.Root.GetAppender(LogName);
                fileAppender.File = $"{filePath}{Path.DirectorySeparatorChar}{fileName}{DEFAULT_EXE}";
                fileAppender.ActivateOptions();
            }

            /// <summary>
            /// log4net.Core.Level.Info 레벨의 로그를 쓰는 함수
            /// </summary>
            /// <param name="message">로그 메시지 오브젝트(ToString 호출)</param>
            public void Info(object message)
            {
                if (!IsLogUsed)
                {
                    return;
                }

                MethodBase methodBase = new StackTrace().GetFrame(1)!.GetMethod()!;
                _logger.Info($"[{methodBase.DeclaringType!.Name}:{methodBase.Name}] {message}");
            }

            /// <summary>
            /// log4net.Core.Level.Info 레벨의 로그를 쓰는 함수 <br/>
            /// (System.Exception 스택 추적을 포함)
            /// </summary>
            /// <param name="message">로그 메시지 오브젝트(ToString 호출)</param>
            /// <param name="exception">스택 추적을 포함한 Exception</param>
            public void Info(object message, Exception exception)
            {
                if (!IsLogUsed)
                {
                    return;
                }

                MethodBase methodBase = new StackTrace().GetFrame(1)!.GetMethod()!;
                _logger.Info($"[{methodBase.DeclaringType!.Name}:{methodBase.Name}] {message}", exception);
            }

            /// <summary>
            /// log4net.Core.Level.Info 레벨의 로그를 쓰는 함수
            /// </summary>
            /// <param name="format">로그 String Format</param>
            /// <param name="args">String Format에 들어갈 매개변수들</param>
            public void Info(string format, params object[] args)
            {
                if (!IsLogUsed)
                {
                    return;
                }

                MethodBase methodBase = new StackTrace().GetFrame(1)!.GetMethod()!;
                _logger.InfoFormat($"[{methodBase.DeclaringType!.Name}:{methodBase.Name}] {format}", args);
            }

            /// <summary>
            /// log4net.Core.Level.Info 레벨의 로그를 쓰는 함수
            /// </summary>
            /// <param name="provider">IFormatProvider</param>
            /// <param name="format">로그 String Format</param>
            /// <param name="args">String Format에 들어갈 매개변수들</param>
            public void Info(IFormatProvider provider, string format, params object[] args)
            {
                if (!IsLogUsed)
                {
                    return;
                }

                MethodBase methodBase = new StackTrace().GetFrame(1)!.GetMethod()!;
                _logger.InfoFormat(provider, $"[{methodBase.DeclaringType!.Name}:{methodBase.Name}] {format}", args);
            }

            /// <summary>
            /// log4net.Core.Level.Debug 레벨의 로그를 쓰는 함수
            /// </summary>
            /// <param name="message">로그 메시지 오브젝트(ToString 호출)</param>
            public void Debug(object message)
            {
                if (!IsLogUsed)
                {
                    return;
                }

                MethodBase methodBase = new StackTrace().GetFrame(1)!.GetMethod()!;
                _logger.Debug($"[{methodBase.DeclaringType!.Name}:{methodBase.Name}] {message}");
            }

            /// <summary>
            /// log4net.Core.Level.Debug 레벨의 로그를 쓰는 함수 <br/>
            /// (System.Exception 스택 추적을 포함)
            /// </summary>
            /// <param name="message">로그 메시지 오브젝트(ToString 호출)</param>
            /// <param name="exception">스택 추적을 포함한 Exception</param>
            public void Debug(object message, Exception exception)
            {
                if (!IsLogUsed)
                {
                    return;
                }

                MethodBase methodBase = new StackTrace().GetFrame(1)!.GetMethod()!;
                _logger.Debug($"[{methodBase.DeclaringType!.Name}:{methodBase.Name}] {message}", exception);
            }

            /// <summary>
            /// log4net.Core.Level.Debug 레벨의 로그를 쓰는 함수
            /// </summary>
            /// <param name="format">로그 String Format</param>
            /// <param name="args">String Format에 들어갈 매개변수들</param>
            public void Debug(string format, params object[] args)
            {
                if (!IsLogUsed)
                {
                    return;
                }

                MethodBase methodBase = new StackTrace().GetFrame(1)!.GetMethod()!;
                _logger.DebugFormat($"[{methodBase.DeclaringType!.Name}:{methodBase.Name}] {format}", args);
            }

            /// <summary>
            /// log4net.Core.Level.Debug 레벨의 로그를 쓰는 함수
            /// </summary>
            /// <param name="provider">IFormatProvider</param>
            /// <param name="format">로그 String Format</param>
            /// <param name="args">String Format에 들어갈 매개변수들</param>
            public void Debug(IFormatProvider provider, string format, params object[] args)
            {
                if (!IsLogUsed)
                {
                    return;
                }

                MethodBase methodBase = new StackTrace().GetFrame(1)!.GetMethod()!;
                _logger.DebugFormat(provider, $"[{methodBase.DeclaringType!.Name}:{methodBase.Name}] {format}", args);
            }

            /// <summary>
            /// log4net.Core.Level.Fatal 레벨의 로그를 쓰는 함수
            /// </summary>
            /// <param name="message">로그 메시지 오브젝트(ToString 호출)</param>
            public void Fatal(object message)
            {
                if (!IsLogUsed)
                {
                    return;
                }

                MethodBase methodBase = new StackTrace().GetFrame(1)!.GetMethod()!;
                _logger.Fatal($"[{methodBase.DeclaringType!.Name}:{methodBase.Name}] {message}");
            }

            /// <summary>
            /// log4net.Core.Level.Fatal 레벨의 로그를 쓰는 함수 <br/>
            /// (System.Exception 스택 추적을 포함)
            /// </summary>
            /// <param name="message">로그 메시지 오브젝트(ToString 호출)</param>
            /// <param name="exception">스택 추적을 포함한 Exception</param>
            public void Fatal(object message, Exception exception)
            {
                if (!IsLogUsed)
                {
                    return;
                }

                MethodBase methodBase = new StackTrace().GetFrame(1)!.GetMethod()!;
                _logger.Fatal($"[{methodBase.DeclaringType!.Name}:{methodBase.Name}] {message}", exception);
            }

            /// <summary>
            /// log4net.Core.Level.Fatal 레벨의 로그를 쓰는 함수
            /// </summary>
            /// <param name="format">로그 String Format</param>
            /// <param name="args">String Format에 들어갈 매개변수들</param>
            public void Fatal(string format, params object[] args)
            {
                if (!IsLogUsed)
                {
                    return;
                }

                MethodBase methodBase = new StackTrace().GetFrame(1)!.GetMethod()!;
                _logger.FatalFormat($"[{methodBase.DeclaringType!.Name}:{methodBase.Name}] {format}", args);
            }

            /// <summary>
            /// log4net.Core.Level.Fatal 레벨의 로그를 쓰는 함수
            /// </summary>
            /// <param name="provider">IFormatProvider</param>
            /// <param name="format">로그 String Format</param>
            /// <param name="args">String Format에 들어갈 매개변수들</param>
            public void Fatal(IFormatProvider provider, string format, params object[] args)
            {
                if (!IsLogUsed)
                {
                    return;
                }

                MethodBase methodBase = new StackTrace().GetFrame(1)!.GetMethod()!;
                _logger.FatalFormat(provider, $"[{methodBase.DeclaringType!.Name}:{methodBase.Name}] {format}", args);
            }

            /// <summary>
            /// log4net.Core.Level.Error 레벨의 로그를 쓰는 함수
            /// </summary>
            /// <param name="message">로그 메시지 오브젝트(ToString 호출)</param>
            public void Error(object message)
            {
                if (!IsLogUsed)
                {
                    return;
                }

                MethodBase methodBase = new StackTrace().GetFrame(1)!.GetMethod()!;
                _logger.Error($"[{methodBase.DeclaringType!.Name}:{methodBase.Name}] {message}");
            }

            /// <summary>
            /// log4net.Core.Level.Error 레벨의 로그를 쓰는 함수 <br/>
            /// </summary>
            /// <param name="message">로그 메시지 오브젝트(ToString 호출)</param>
            /// <param name="exception">스택 추적을 포함한 Exception</param>
            public void Error(object message, Exception exception)
            {
                if (!IsLogUsed)
                {
                    return;
                }

                MethodBase methodBase = new StackTrace().GetFrame(1)!.GetMethod()!;
                _logger.Error($"[{methodBase.DeclaringType!.Name}:{methodBase.Name}] {message}", exception);
            }

            /// <summary>
            /// log4net.Core.Level.Error 레벨의 로그를 쓰는 함수
            /// </summary>
            /// <param name="format">로그 String Format</param>
            /// <param name="args">String Format에 들어갈 매개변수들</param>
            public void Error(string format, params object[] args)
            {
                if (!IsLogUsed)
                {
                    return;
                }

                MethodBase methodBase = new StackTrace().GetFrame(1)!.GetMethod()!;
                _logger.ErrorFormat($"[{methodBase.DeclaringType!.Name}:{methodBase.Name}] {format}", args);
            }

            /// <summary>
            /// log4net.Core.Level.Error 레벨의 로그를 쓰는 함수
            /// </summary>
            /// <param name="provider">IFormatProvider</param>
            /// <param name="format">로그 String Format</param>
            /// <param name="args">String Format에 들어갈 매개변수들</param>
            public void Error(IFormatProvider provider, string format, params object[] args)
            {
                if (!IsLogUsed)
                {
                    return;
                }

                MethodBase methodBase = new StackTrace().GetFrame(1)!.GetMethod()!;
                _logger.ErrorFormat(provider, $"[{methodBase.DeclaringType!.Name}:{methodBase.Name}] {format}", args);
            }

            /// <summary>
            /// 로그 폴더 초기 설정
            /// </summary>
            /// <returns>
            /// true : 성공 <br/>
            /// false : 실패
            /// </returns>
            private bool InitLogFolder()
            {
                if (!Directory.Exists(LogFolderPath))
                {
                    DirectoryInfo dirInfo = Directory.CreateDirectory(LogFolderPath);

                    if (!dirInfo.Exists)
                    {
                        WriteInternalLog($"Log folder({LogFolderPath}) create failed");
                        return false;
                    }
                }

                if (!Directory.Exists(LogFullPath))
                {
                    DirectoryInfo dirInfo = Directory.CreateDirectory(LogFullPath);

                    if (!dirInfo.Exists)
                    {
                        WriteInternalLog($"Log folder({LogFullPath}) create failed");
                        return false;
                    }
                }

                return true;
            }

            /// <summary>
            /// 내부 로그
            /// </summary>
            /// <param name="message"></param>
            private void WriteInternalLog(string message)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine(message);
#endif
            }
        }

        /// <summary>
        /// log4net에서 사용할 비동기 Appender
        /// </summary>
        public sealed class AsyncAppender :
            log4net.Appender.IAppender, log4net.Appender.IBulkAppender, log4net.Core.IOptionHandler, log4net.Core.IAppenderAttachable
        {
            /// <summary>
            /// Logger의 이름
            /// </summary>
            public string Name { get; set; } = string.Empty;
            /// <summary>
            /// Logger의 FixFlag
            /// </summary>
            public log4net.Core.FixFlags Fix
            {
                get { return _fixFlag; }
                set { _fixFlag = value; }
            }

            public log4net.Appender.AppenderCollection Appenders
            {
                get
                {
                    lock (this)
                    {
                        return _appenderAttachedImpl == null ? log4net.Appender.AppenderCollection.EmptyCollection : _appenderAttachedImpl.Appenders;
                    }
                }
            }

            private log4net.Util.AppenderAttachedImpl? _appenderAttachedImpl = null;
            private log4net.Core.FixFlags _fixFlag = log4net.Core.FixFlags.All;

            public void ActivateOptions()
            {

            }

            public void AddAppender(log4net.Appender.IAppender appender)
            {
                if (appender == null)
                {
                    throw new ArgumentNullException(nameof(appender));
                }

                lock (this)
                {
                    _appenderAttachedImpl ??= new log4net.Util.AppenderAttachedImpl();
                    _appenderAttachedImpl.AddAppender(appender);
                }
            }

            public void Close()
            {
                lock (this)
                {
                    _appenderAttachedImpl?.RemoveAllAppenders();
                }
            }

            public void DoAppend(log4net.Core.LoggingEvent loggingEvent)
            {
                loggingEvent.Fix = _fixFlag;
                ThreadPool.QueueUserWorkItem(new WaitCallback(AsyncAppend), loggingEvent);
            }

            public void DoAppend(log4net.Core.LoggingEvent[] loggingEvents)
            {
                for (int i = 0; i < loggingEvents.Length; ++i)
                {
                    loggingEvents[i].Fix = _fixFlag;
                }
                ThreadPool.QueueUserWorkItem(new WaitCallback(AsyncAppend), loggingEvents);
            }

            public log4net.Appender.IAppender? GetAppender(string name)
            {
                lock (this)
                {
                    if (_appenderAttachedImpl == null ||
                        string.IsNullOrEmpty(name))
                    {
                        return null;
                    }

                    return _appenderAttachedImpl.GetAppender(name);
                }
            }

            public void RemoveAllAppenders()
            {
                lock (this)
                {
                    if (_appenderAttachedImpl != null)
                    {
                        _appenderAttachedImpl.RemoveAllAppenders();
                        _appenderAttachedImpl = null;
                    }
                }
            }

            public log4net.Appender.IAppender? RemoveAppender(log4net.Appender.IAppender appender)
            {
                lock (this)
                {
                    if (appender != null &&
                        _appenderAttachedImpl != null)
                    {
                        return _appenderAttachedImpl.RemoveAppender(appender);
                    }
                }

                return null;
            }

            public log4net.Appender.IAppender? RemoveAppender(string name)
            {
                lock (this)
                {
                    if (!string.IsNullOrEmpty(name) &&
                        _appenderAttachedImpl != null)
                    {
                        return _appenderAttachedImpl.RemoveAppender(name);
                    }
                }

                return null;
            }

            private void AsyncAppend(object? state)
            {
                if (_appenderAttachedImpl == null)
                {
                    return;
                }

                if (state is log4net.Core.LoggingEvent loggingEvent)
                {
                    _appenderAttachedImpl.AppendLoopOnAppenders(loggingEvent);
                }
                else if (state is log4net.Core.LoggingEvent[] loggingEvents)
                {
                    _appenderAttachedImpl.AppendLoopOnAppenders(loggingEvents);
                }
            }
        }
    }
    
}
