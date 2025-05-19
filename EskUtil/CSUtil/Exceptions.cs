// ======================================================================================================
// File Name        : Exceptions.cs
// Project          : CSUtil
// Last Update      : 2025.05.19 - yc.jeon
// ======================================================================================================

using System;

namespace CSUtil
{
    /// <summary>
    /// 올바르지 않은 Type으로 인한 예외
    /// </summary>
    public class InvalidTypeException : ArgumentException
    {
        public InvalidTypeException()
            : base()
        {

        }

        public InvalidTypeException(string message)
            : base(message)
        {

        }

        public InvalidTypeException(string message, string paramName)
            : base(message, paramName)
        {

        }
    }

    /// <summary>
    /// 데이터의 값이 null 임으로 인한 예외
    /// </summary>
    public class NullException : ArgumentException
    {
        public NullException()
            : base()
        {

        }

        public NullException(string message)
            : base(message)
        {

        }

        public NullException(string message, string paramName)
            : base(message, paramName)
        {

        }
    }

    /// <summary>
    /// 객체 생성에 실패했을 때 발생한 예외
    /// </summary>
    public class CreateException : Exception
    {
        public CreateException()
            : base()
        {

        }

        public CreateException(string message)
            : base(message)
        {
        }

        public CreateException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    /// <summary>
    /// 생성자를 정상적으로 수행하지 못했을 때 발생하는 Exception
    /// </summary>
    public class ConstructFailException : Exception
    {
        public ConstructFailException()
            : base()
        {
        }

        public ConstructFailException(string message)
            : base(message)
        {
        }

        public ConstructFailException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    /// <summary>
    /// Initialize와 같이 초기화와 동일한 동작이거나 유사한 동작을 수행 도중에 정상적이지 않은 문제로 인해 발생한 예외
    /// </summary>
    public class InitializeException : Exception
    {
        public InitializeException()
            : base()
        {

        }

        public InitializeException(string message)
            : base(message)
        {

        }

        public InitializeException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }

    /// <summary>
    /// Array의 Length가 0으로인해 발생한 예외
    /// </summary>
    public class ArrayLengthZeroException : Exception
    {
        public ArrayLengthZeroException()
            : base()
        {

        }

        public ArrayLengthZeroException(string message)
            : base(message)
        {

        }

        public ArrayLengthZeroException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }

    /// <summary>
    /// String의 값이 Null 혹은 Empty인 경우 발생한 예외
    /// </summary>
    public class StringNullEmptyException : ArgumentException
    {
        public StringNullEmptyException()
            : base()
        {

        }

        public StringNullEmptyException(string message)
            : base(message)
        {

        }

        public StringNullEmptyException(string message, string paramName)
            : base(message, paramName)
        {

        }
    }
}
