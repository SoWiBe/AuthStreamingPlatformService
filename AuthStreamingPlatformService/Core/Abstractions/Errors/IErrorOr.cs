﻿namespace TechDaily.Core.Abstractions.Errors;

public interface IErrorOr
{
    List<Error>? Errors { get; }
    bool IsError { get; }
}