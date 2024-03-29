﻿using System.Runtime.Serialization;

namespace Common.Exceptions;

[Serializable]
public class ValidationException: Exception
{
    public ValidationException():base() { }
    public ValidationException(string message) : base(message) { }
    public ValidationException(string message,  Exception innerException) : base(message, innerException) { }
    public ValidationException(SerializationInfo info, StreamingContext context):base(info, context) { }
}