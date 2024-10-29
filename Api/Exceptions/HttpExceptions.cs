namespace Api.Exceptions;

public class NotFoundException(string message, Exception? exception = default) : Exception(message, exception);

public class BadRequestException(string message, Exception? exception = default) : Exception(message, exception);

public class ConflictException(string message, Exception? exception = default) : Exception(message, exception);

public class ForbiddenException(string message, Exception? exception = default) : Exception(message, exception);