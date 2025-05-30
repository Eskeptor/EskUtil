﻿// ======================================================================================================
// File Name        : Log4netUtil.cs
// Project          : CSUtil
// Last Update      : 2025.05.19 - yc.jeon
// ======================================================================================================

using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

using log4net.Appender;
using log4net.Layout;

namespace CSUtil
{
    /// <summary>
    /// log4net용 유틸리티
    /// </summary>
    namespace Log4netUtil
    {
        /// <summary>
        /// Log Manager (using Log4Net)
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
                public bool IsAppendToFile { get; set; }
                /// <summary>
                /// 최대 RollBackups 크기
                /// </summary>
                public int MaxSizeRollBackups { get; set; } = 5;
                /// <summary>
                /// 최대 파일 크기
                /// </summary>
                public string MaximumFileSize { get; set; } = "10MB";
                /// <summary>
                /// Rolling Mode
                /// </summary>
                public RollingFileAppender.RollingMode RollingMode { get; set; } = RollingFileAppender.RollingMode.Composite;
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
            public string LogFolderPath { get; }
            /// <summary>
            /// 로그 이름
            /// </summary>
            public string LogName { get; }
            /// <summary>
            /// 로그 전체 경로 (LogFolderPath + LogName) <br/>
            /// (예: C:\MyProject\MyLog\) <br/>
            /// (단, 생성자 인자가 4개짜리로 생성된 로그의 경우 LogFolderPath와 동일함)
            /// </summary>
            public string LogFolderFullPath { get; }
            /// <summary>
            /// 로그 사용 유무 (변경은 생성자에서만 가능)
            /// </summary>
            public bool IsLogUsed { get; }
            /// <summary>
            /// 로그 파일의 헤더
            /// </summary>
            public string LogFileHeader { get; }
            /// <summary>
            /// 로그 파일의 파일 확장자
            /// </summary>
            public string LogFormat { get; }
            /// <summary>
            /// Log Manager 초기화시 발생하는 로그를 기록할지 유무
            /// </summary>
            private bool IsUseInitLog { get; }
            /// <summary>
            /// Log4Net Logger
            /// </summary>
            private log4net.ILog _logger;
            /// <summary>
            /// Logger Shutdown 유무
            /// </summary>
            public bool IsShutdown { get => _isShutdown; }
            private bool _isShutdown;
            /// <summary>
            /// 로그 파일의 전체 경로 (LogFolderFullPath + LogFileFullName) <br/>
            /// (예: C:\MyProject\MyLog\Header_Log_2024-01-01.log)
            /// </summary>
            public string LogFileFullPath { get; }
            /// <summary>
            /// 로그 파일의 최종 생성된 이름 (LogFileFullPath에서 파일 이름 + 확장자만 떼온 것) <br/>
            /// (예: Header_Log_2024-01-01.log)
            /// </summary>
            public string LogFileFullName { get; }

            /// <summary>
            /// 생성자 (FileAppender)
            /// </summary>
            /// <param name="logFolderPath">로그 폴더 경로</param>
            /// <param name="logName">로그 이름</param>
            /// <param name="logFormat"></param>
            /// <param name="isUseInitLog"></param>
            /// <exception cref="ArgumentNullException"></exception>
            public LogManager(string logFolderPath, string logName, string logFormat = DEFAULT_EXE, bool isUseInitLog = true)
            {
                if (string.IsNullOrWhiteSpace(logFolderPath))
                {
                    throw new ArgumentNullException(nameof(logFolderPath));
                }
                if (string.IsNullOrWhiteSpace(logName))
                {
                    throw new ArgumentNullException(nameof(logName));
                }

                if (!Directory.Exists(logFolderPath))
                {
                    Directory.CreateDirectory(logFolderPath);
                }
                LogFolderPath = logFolderPath;
                LogFolderFullPath = logFolderPath;
                IsLogUsed = true;
                LogFileHeader = string.Empty;
                IsUseInitLog = isUseInitLog;
                LogFileFullName = $"{logName}{logFormat}";
                LogFileFullPath = Path.Combine(logFolderPath, LogFileFullName);
                LogName = LogFileFullPath;

                log4net.Repository.ILoggerRepository repository;
                try
                {
                    repository = log4net.LogManager.CreateRepository(LogName);
                    FileAppender fileAppender = new FileAppender()
                    {
                        Name = logName,
                        File = LogFileFullPath,
                        AppendToFile = true,
                        Layout = new PatternLayout
                        {
                            ConversionPattern = "%message%newline"
                        },
                        Encoding = Encoding.UTF8,
                    };
                    ((PatternLayout)fileAppender.Layout).ActivateOptions();
                    fileAppender.ActivateOptions();
                    log4net.Config.BasicConfigurator.Configure(repository, fileAppender);
                }
                catch (Exception)
                {
                    repository = log4net.LogManager.GetRepository(LogName);
                }
                _logger = log4net.LogManager.GetLogger(LogName, LogName);
                if (IsUseInitLog)
                {
                    Info("New log start");
                }
            }

            /// <summary>
            /// 생성자
            /// </summary>
            /// <param name="logFolderPath">로그 폴더 경로</param>
            /// <param name="logName">로그 이름</param>
            /// <param name="conversionPattern">Conversion Pattern (For Log4Net RollingFileAppender)</param>
            /// <param name="datePattern">Date Pattern (For Log4Net RollingFileAppender)</param>
            /// <param name="rollingSettting">RollingFileAppender 설정값</param>
            /// <param name="isLogUsed">로그 사용 유무</param>
            /// <param name="logFormat">로그 파일의 확장자</param>
            /// <param name="isUseInitLog">Log Manager 초기화시 발생하는 로그를 기록할지 유무</param>
            /// <param name="isUseDefenseDup">Log 이름 겹침 방지 사용 유무</param>
            /// <exception cref="ArgumentNullException"></exception>
            /// <exception cref="DirectoryNotFoundException"></exception>
            public LogManager(string logFolderPath, string logName, string conversionPattern, string datePattern, LogRollingSettting rollingSettting, bool isLogUsed, string logFormat = DEFAULT_EXE, bool isUseInitLog = true, bool isUseDefenseDup = false)
                : this(logFolderPath, logName, "", conversionPattern, datePattern, rollingSettting, isLogUsed, logFormat, isUseInitLog, isUseDefenseDup)
            {

            }

            /// <summary>
            /// 생성자
            /// </summary>
            /// <param name="logFolderPath">로그 폴더 경로</param>
            /// <param name="logName">로그 이름</param>
            /// <param name="fileHeader">로그 파일의 헤더</param>
            /// <param name="conversionPattern">Conversion Pattern (For Log4Net RollingFileAppender)</param>
            /// <param name="datePattern">Date Pattern (For Log4Net RollingFileAppender)</param>
            /// <param name="rollingSettting">RollingFileAppender 설정값</param>
            /// <param name="isLogUsed">로그 사용 유무</param>
            /// <param name="logFormat">로그 파일의 확장자</param>
            /// <param name="isUseInitLog">Log Manager 초기화시 발생하는 로그를 기록할지 유무</param>
            /// <param name="isUseDefenseDup">Log 이름 겹침 방지 사용 유무</param>
            /// <exception cref="ArgumentNullException"></exception>
            /// <exception cref="DirectoryNotFoundException"></exception>
            public LogManager(string logFolderPath, string logName, string fileHeader, string conversionPattern, string datePattern, LogRollingSettting rollingSettting, bool isLogUsed, string logFormat = DEFAULT_EXE, bool isUseInitLog = true, bool isUseDefenseDup = false)
            {
                IsLogUsed = isLogUsed;
                IsUseInitLog = isUseInitLog;
                LogFileHeader = fileHeader;
                if (!IsLogUsed)
                {
                    System.Diagnostics.Debug.WriteLine("[Common.Log.LogManager:.ctor] Log not used.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(logFolderPath))
                {
                    System.Diagnostics.Debug.WriteLine($"[Common.Log.LogManager:.ctor] Failed: {nameof(logFolderPath)} is null or empty.");
                    throw new ArgumentNullException(nameof(logFolderPath), "Log folder path is null or empty.");
                }
                if (string.IsNullOrWhiteSpace(logName))
                {
                    System.Diagnostics.Debug.WriteLine($"[Common.Log.LogManager:.ctor] Failed: {nameof(logName)} is null or empty.");
                    throw new ArgumentNullException(nameof(logName), "Log name is null or empty");
                }
                if (rollingSettting == null)
                {
                    System.Diagnostics.Debug.WriteLine($"[Common.Log.LogManager:.ctor] Failed: {nameof(rollingSettting)} is null.");
                    throw new ArgumentNullException(nameof(rollingSettting), "Rolling setting is null.");
                }
                if (string.IsNullOrWhiteSpace(conversionPattern))
                {
                    conversionPattern = DEFAULT_PATTERN_CONVERSION;
                }
                if (string.IsNullOrWhiteSpace(datePattern))
                {
                    datePattern = DEFAULT_PATTERN_DATE;
                }

                LogFormat = logFormat;
                LogFolderPath = logFolderPath;
                LogName = $"{logName}{fileHeader}";
                LogFolderFullPath = $"{logFolderPath}{Path.DirectorySeparatorChar}{logName}{Path.DirectorySeparatorChar}";
                if (!InitLogFolder())
                {
                    throw new DirectoryNotFoundException($"Log folder({LogFolderFullPath}) create failed.");
                }
                if (isUseDefenseDup)
                {
                    LogName = $"{LogName}_{DateTime.Now:yyyyMMddHHmmssfff}";
                }

                LogFileFullName = $"{fileHeader}{DateTime.Now.ToString(datePattern, CultureInfo.InvariantCulture)}{LogFormat}";
                LogFileFullPath = Path.Combine(LogFolderFullPath, LogFileFullName);

                bool firstCreate = false;
                log4net.Repository.ILoggerRepository repository;
                try
                {
                    repository = log4net.LogManager.CreateRepository(LogName);
                    PatternLayout patternLayout = new PatternLayout()
                    {
                        ConversionPattern = conversionPattern,
                    };
                    patternLayout.ActivateOptions();

                    RollingFileAppender rollingAppender = new RollingFileAppender()
                    {
                        Name = LogName,
                        DatePattern = $"'{fileHeader}'{datePattern}'{LogFormat}'",
                        StaticLogFileName = false,
                        AppendToFile = rollingSettting.IsAppendToFile,
                        RollingStyle = rollingSettting.RollingMode,
                        MaxSizeRollBackups = rollingSettting.MaxSizeRollBackups,
                        MaximumFileSize = rollingSettting.MaximumFileSize,
                        Layout = patternLayout,
                        LockingModel = new RollingFileAppender.MinimalLock(),
                        Encoding = Encoding.UTF8,
                    };
                    if (rollingSettting.RollingMode == RollingFileAppender.RollingMode.Date ||
                        rollingSettting.RollingMode == RollingFileAppender.RollingMode.Composite)
                    {
                        rollingAppender.File = LogFolderFullPath;
                    }
                    else
                    {
                        rollingAppender.File = LogFileFullPath;
                    }
                    rollingAppender.ActivateOptions();

                    AsyncAppender asyncAppender = new AsyncAppender();
                    asyncAppender.AddAppender(rollingAppender);
                    asyncAppender.ActivateOptions();

                    //log4net.Config.BasicConfigurator.Configure(repository, asyncAppender, rollingAppender);
                    log4net.Config.BasicConfigurator.Configure(repository, asyncAppender);
                    firstCreate = true;
                }
                catch
                {
                    //Console.WriteLine(ex.ToString());
                    repository = log4net.LogManager.GetRepository(LogName);
                }

                _logger = log4net.LogManager.GetLogger(LogName, LogName);

                if (firstCreate &&
                    IsUseInitLog)
                {
                    Info("New log start");
                }
            }

            /// <summary>
            /// 소멸자
            /// </summary>
            ~LogManager()
            {
                ShutdownManager();
            }

            /// <summary>
            /// 현재 LogManager를 종료하는 함수
            /// </summary>
            public void ShutdownManager()
            {
                if (IsShutdown ||
                    string.IsNullOrWhiteSpace(LogName))
                {
                    return;
                }

                log4net.Repository.ILoggerRepository repository = log4net.LogManager.GetRepository(LogName);
                if (repository != null)
                {
                    log4net.LogManager.ShutdownRepository(LogName);
                    repository.Threshold = log4net.Core.Level.Off;
                }
                _isShutdown = true;
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
                if (!IsLogUsed ||
                    IsShutdown)
                {
                    System.Diagnostics.Debug.WriteLine($"[Common.Log.LogManager:FileNameChange] Failed: Log not used.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(fileName))
                {
                    if (IsUseInitLog)
                    {
                        Error("Filename is null or empty.");
                    }
                    return;
                }
                if (string.IsNullOrWhiteSpace(filePath))
                {
                    filePath = LogFolderFullPath;
                }

                log4net.Repository.Hierarchy.Hierarchy rootHierarchy = (log4net.Repository.Hierarchy.Hierarchy)_logger.Logger.Repository;
                log4net.Appender.FileAppender fileAppender = (log4net.Appender.FileAppender)rootHierarchy.Root.GetAppender(LogName);
                fileAppender.File = $"{filePath}{Path.DirectorySeparatorChar}{fileName}";
                fileAppender.ActivateOptions();
            }

            #region Info Log
            /// <summary>
            /// log4net.Core.Level.Info 레벨의 로그를 쓰는 함수
            /// </summary>
            /// <param name="message">로그 메시지 오브젝트(ToString 호출)</param>
            public void Info(object message)
            {
                if (!IsLogUsed ||
                    IsShutdown)
                {
                    return;
                }

                _logger.Info(message);
            }

            /// <summary>
            /// log4net.Core.Level.Info 레벨의 로그를 쓰는 함수 <br/>
            /// (System.Exception 스택 추적을 포함)
            /// </summary>
            /// <param name="message">로그 메시지 오브젝트(ToString 호출)</param>
            /// <param name="exception">스택 추적을 포함한 Exception</param>
            public void Info(object message, Exception exception)
            {
                if (!IsLogUsed ||
                    IsShutdown)
                {
                    return;
                }

                _logger.Info(message, exception);
            }

            /// <summary>
            /// log4net.Core.Level.Info 레벨의 로그를 쓰는 함수
            /// </summary>
            /// <param name="format">로그 String Format</param>
            /// <param name="args">String Format에 들어갈 매개변수들</param>
            public void Info(string format, params object[] args)
            {
                if (!IsLogUsed ||
                    IsShutdown)
                {
                    return;
                }

                _logger.InfoFormat(CultureInfo.InvariantCulture, format, args);
            }

            /// <summary>
            /// log4net.Core.Level.Info 레벨의 로그를 쓰는 함수 
            /// </summary>
            /// <param name="message">로그 메시지</param>
            public void InfoWithCaller(string message, [CallerMemberName] string caller = null)
            {
                if (!IsLogUsed ||
                    IsShutdown)
                {
                    return;
                }

                _logger.Info($"[{caller}] {message}");
            }

            /// <summary>
            /// log4net.Core.Level.Info 레벨의 로그를 쓰는 함수 
            /// </summary>
            /// <param name="message">로그 메시지</param>
            public void InfoWithCaller(object message, [CallerMemberName] string caller = null)
            {
                if (!IsLogUsed ||
                    IsShutdown)
                {
                    return;
                }

                _logger.Info($"[{caller}] {message}");
            }
            #endregion

            #region Debug Log
            /// <summary>
            /// log4net.Core.Level.Debug 레벨의 로그를 쓰는 함수 <br/>
            /// (Release 모드에서는 로그 기록을 하지 않음) <br/>
            /// </summary>
            /// <param name="message">로그 메시지 오브젝트(ToString 호출)</param>
            [Conditional("DEBUG")]
            public void Debug(object message)
            {
                if (!IsLogUsed ||
                    IsShutdown)
                {
                    return;
                }

                _logger.Debug(message);
            }

            /// <summary>
            /// log4net.Core.Level.Debug 레벨의 로그를 쓰는 함수 <br/>
            /// (System.Exception 스택 추적을 포함)
            /// (Release 모드에서는 로그 기록을 하지 않음) <br/>
            /// </summary>
            /// <param name="message">로그 메시지 오브젝트(ToString 호출)</param>
            /// <param name="exception">스택 추적을 포함한 Exception</param>
            [Conditional("DEBUG")]
            public void Debug(object message, Exception exception)
            {
                if (!IsLogUsed ||
                    IsShutdown)
                {
                    return;
                }

                _logger.Debug(message, exception);
            }

            /// <summary>
            /// log4net.Core.Level.Debug 레벨의 로그를 쓰는 함수
            /// (Release 모드에서는 로그 기록을 하지 않음) <br/>
            /// </summary>
            /// <param name="format">로그 String Format</param>
            /// <param name="args">String Format에 들어갈 매개변수들</param>
            [Conditional("DEBUG")]
            public void Debug(string format, params object[] args)
            {
                if (!IsLogUsed ||
                    IsShutdown)
                {
                    return;
                }

                _logger.DebugFormat(CultureInfo.InvariantCulture, format, args);
            }

            /// <summary>
            /// log4net.Core.Level.Debug 레벨의 로그를 쓰는 함수 
            /// (Release 모드에서는 로그 기록을 하지 않음) <br/>
            /// </summary>
            /// <param name="message">로그 메시지</param>
            [Conditional("DEBUG")]
            public void DebugWithCaller(string message, [CallerMemberName] string caller = null)
            {
                if (!IsLogUsed ||
                    IsShutdown)
                {
                    return;
                }

                _logger.Debug($"[{caller}] {message}");
            }

            /// <summary>
            /// log4net.Core.Level.Debug 레벨의 로그를 쓰는 함수 
            /// (Release 모드에서는 로그 기록을 하지 않음) <br/>
            /// </summary>
            /// <param name="message">로그 메시지</param>
            [Conditional("DEBUG")]
            public void DebugWithCaller(object message, [CallerMemberName] string caller = null)
            {
                if (!IsLogUsed ||
                    IsShutdown)
                {
                    return;
                }

                _logger.Debug($"[{caller}] {message}");
            }
            #endregion

            #region Fatal Log
            /// <summary>
            /// log4net.Core.Level.Fatal 레벨의 로그를 쓰는 함수
            /// </summary>
            /// <param name="message">로그 메시지 오브젝트(ToString 호출)</param>
            public void Fatal(object message)
            {
                if (!IsLogUsed ||
                    IsShutdown)
                {
                    return;
                }

                _logger.Fatal(message);
            }

            /// <summary>
            /// log4net.Core.Level.Fatal 레벨의 로그를 쓰는 함수 <br/>
            /// (System.Exception 스택 추적을 포함)
            /// </summary>
            /// <param name="message">로그 메시지 오브젝트(ToString 호출)</param>
            /// <param name="exception">스택 추적을 포함한 Exception</param>
            public void Fatal(object message, Exception exception)
            {
                if (!IsLogUsed ||
                    IsShutdown)
                {
                    return;
                }

                _logger.Fatal(message, exception);
            }

            /// <summary>
            /// log4net.Core.Level.Fatal 레벨의 로그를 쓰는 함수
            /// </summary>
            /// <param name="format">로그 String Format</param>
            /// <param name="args">String Format에 들어갈 매개변수들</param>
            public void Fatal(string format, params object[] args)
            {
                if (!IsLogUsed ||
                    IsShutdown)
                {
                    return;
                }

                _logger.FatalFormat(CultureInfo.InvariantCulture, format, args);
            }

            /// <summary>
            /// log4net.Core.Level.Fatal 레벨의 로그를 쓰는 함수 
            /// </summary>
            /// <param name="message">로그 메시지</param>
            public void FatalWithCaller(string message, [CallerMemberName] string caller = null)
            {
                if (!IsLogUsed ||
                    IsShutdown)
                {
                    return;
                }

                _logger.Fatal($"[{caller}] {message}");
            }

            /// <summary>
            /// log4net.Core.Level.Fatal 레벨의 로그를 쓰는 함수 
            /// </summary>
            /// <param name="message">로그 메시지</param>
            public void FatalWithCaller(object message, [CallerMemberName] string caller = null)
            {
                if (!IsLogUsed ||
                    IsShutdown)
                {
                    return;
                }

                _logger.Fatal($"[{caller}] {message}");
            }
            #endregion

            #region Error Log
            /// <summary>
            /// log4net.Core.Level.Error 레벨의 로그를 쓰는 함수
            /// </summary>
            /// <param name="message">로그 메시지 오브젝트(ToString 호출)</param>
            public void Error(object message)
            {
                if (!IsLogUsed ||
                    IsShutdown)
                {
                    return;
                }

                _logger.Error(message);
            }

            /// <summary>
            /// log4net.Core.Level.Error 레벨의 로그를 쓰는 함수 <br/>
            /// </summary>
            /// <param name="message">로그 메시지 오브젝트(ToString 호출)</param>
            /// <param name="exception">스택 추적을 포함한 Exception</param>
            public void Error(object message, Exception exception)
            {
                if (!IsLogUsed ||
                    IsShutdown)
                {
                    return;
                }

                _logger.Error(message, exception);
            }

            /// <summary>
            /// log4net.Core.Level.Error 레벨의 로그를 쓰는 함수
            /// </summary>
            /// <param name="format">로그 String Format</param>
            /// <param name="args">String Format에 들어갈 매개변수들</param>
            public void Error(string format, params object[] args)
            {
                if (!IsLogUsed ||
                    IsShutdown)
                {
                    return;
                }

                _logger.ErrorFormat(CultureInfo.InvariantCulture, format, args);
            }

            /// <summary>
            /// log4net.Core.Level.Error 레벨의 로그를 쓰는 함수 
            /// </summary>
            /// <param name="message">로그 메시지</param>
            public void ErrorWithCaller(string message, [CallerMemberName] string caller = null)
            {
                if (!IsLogUsed ||
                    IsShutdown)
                {
                    return;
                }

                _logger.Error($"[{caller}] {message}");
            }

            /// <summary>
            /// log4net.Core.Level.Error 레벨의 로그를 쓰는 함수 
            /// </summary>
            /// <param name="message">로그 메시지</param>
            public void ErrorWithCaller(object message, [CallerMemberName] string caller = null)
            {
                if (!IsLogUsed ||
                    IsShutdown)
                {
                    return;
                }

                _logger.Error($"[{caller}] {message}");
            }
            #endregion

            #region Warning Log
            /// <summary>
            /// log4net.Core.Level.Warn 레벨의 로그를 쓰는 함수
            /// </summary>
            /// <param name="message">로그 메시지 오브젝트(ToString 호출)</param>
            public void Warn(object message)
            {
                if (!IsLogUsed ||
                    IsShutdown)
                {
                    return;
                }

                _logger.Warn(message);
            }

            /// <summary>
            /// log4net.Core.Level.Warn 레벨의 로그를 쓰는 함수 <br/>
            /// </summary>
            /// <param name="message">로그 메시지 오브젝트(ToString 호출)</param>
            /// <param name="exception">스택 추적을 포함한 Exception</param>
            public void Warn(object message, Exception exception)
            {
                if (!IsLogUsed ||
                    IsShutdown)
                {
                    return;
                }

                _logger.Warn(message, exception);
            }

            /// <summary>
            /// log4net.Core.Level.Warn 레벨의 로그를 쓰는 함수
            /// </summary>
            /// <param name="format">로그 String Format</param>
            /// <param name="args">String Format에 들어갈 매개변수들</param>
            public void Warn(string format, params object[] args)
            {
                if (!IsLogUsed ||
                    IsShutdown)
                {
                    return;
                }

                _logger.WarnFormat(CultureInfo.InvariantCulture, format, args);
            }

            /// <summary>
            /// log4net.Core.Level.Warn 레벨의 로그를 쓰는 함수 
            /// </summary>
            /// <param name="message">로그 메시지</param>
            public void WarnWithCaller(string message, [CallerMemberName] string caller = null)
            {
                if (!IsLogUsed ||
                    IsShutdown)
                {
                    return;
                }

                _logger.Warn($"[{caller}] {message}");
            }

            /// <summary>
            /// log4net.Core.Level.Warn 레벨의 로그를 쓰는 함수 
            /// </summary>
            /// <param name="message">로그 메시지</param>
            public void WarnWithCaller(object message, [CallerMemberName] string caller = null)
            {
                if (!IsLogUsed ||
                    IsShutdown)
                {
                    return;
                }

                _logger.Warn($"[{caller}] {message}");
            }
            #endregion

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
                        System.Diagnostics.Debug.WriteLine($"[Common.Log.LogManager:InitLogFolder] Failed: Log folder({LogFolderPath}) create failed.");
                        return false;
                    }
                }

                if (!Directory.Exists(LogFolderFullPath))
                {
                    DirectoryInfo dirInfo = Directory.CreateDirectory(LogFolderFullPath);

                    if (!dirInfo.Exists)
                    {
                        System.Diagnostics.Debug.WriteLine($"[Common.Log.LogManager:InitLogFolder] Failed: Log folder({LogFolderFullPath}) create failed.");
                        return false;
                    }
                }

                return true;
            }

            //private static string GetCurrentMethodCaller([CallerMemberName] string methodName = null)
            //{
            //    return methodName;
            //}
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
            public log4net.Core.FixFlags Fix { get; set; } = log4net.Core.FixFlags.All;

            private object Locker { get; } = new object();

            public log4net.Appender.AppenderCollection Appenders
            {
                get
                {
                    lock (Locker)
                    {
                        return _appenderAttachedImpl == null ? log4net.Appender.AppenderCollection.EmptyCollection : _appenderAttachedImpl.Appenders;
                    }
                }
            }

            private log4net.Util.AppenderAttachedImpl _appenderAttachedImpl;

            public void ActivateOptions()
            {

            }

            public void AddAppender(log4net.Appender.IAppender appender)
            {
                lock (Locker)
                {
                    if (_appenderAttachedImpl == null)
                    {
                        _appenderAttachedImpl = new log4net.Util.AppenderAttachedImpl();
                    }
                    _appenderAttachedImpl.AddAppender(appender);
                }
            }

            public void Close()
            {
                lock (Locker)
                {
                    _appenderAttachedImpl?.RemoveAllAppenders();
                }
            }

            public void DoAppend(log4net.Core.LoggingEvent loggingEvent)
            {
                loggingEvent.Fix = Fix;
                ThreadPool.QueueUserWorkItem(new WaitCallback(AsyncAppend), loggingEvent);
            }

            public void DoAppend(log4net.Core.LoggingEvent[] loggingEvents)
            {
                for (int i = 0; i < loggingEvents.Length; ++i)
                {
                    loggingEvents[i].Fix = Fix;
                }
                ThreadPool.QueueUserWorkItem(new WaitCallback(AsyncAppend), loggingEvents);
            }

            public log4net.Appender.IAppender GetAppender(string name)
            {
                lock (Locker)
                {
                    if (_appenderAttachedImpl == null ||
                        string.IsNullOrWhiteSpace(name))
                    {
                        return null;
                    }

                    return _appenderAttachedImpl.GetAppender(name);
                }
            }

            public void RemoveAllAppenders()
            {
                lock (Locker)
                {
                    if (_appenderAttachedImpl != null)
                    {
                        _appenderAttachedImpl.RemoveAllAppenders();
                        _appenderAttachedImpl = null;
                    }
                }
            }

            public log4net.Appender.IAppender RemoveAppender(log4net.Appender.IAppender appender)
            {
                lock (Locker)
                {
                    if (appender != null &&
                        _appenderAttachedImpl != null)
                    {
                        return _appenderAttachedImpl.RemoveAppender(appender);
                    }
                }

                return null;
            }

            public log4net.Appender.IAppender RemoveAppender(string name)
            {
                lock (Locker)
                {
                    if (!string.IsNullOrWhiteSpace(name) &&
                        _appenderAttachedImpl != null)
                    {
                        return _appenderAttachedImpl.RemoveAppender(name);
                    }
                }

                return null;
            }

            private void AsyncAppend(object state)
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
