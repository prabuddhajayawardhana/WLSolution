namespace WLSolution.SharedKernel.Validation;

public static class Guard
{
    /// <summary>
    /// Throws an ArgumentNullException if the provided object is null.
    /// </summary>
    /// <param name="obj">The object to check.</param>
    /// <param name="parameterName">The name of the parameter being checked (for clearer exception messages).</param>
    public static void ThrowIfNull(object obj, string parameterName)
    {
        if (obj == null)
        {
            throw new ArgumentNullException(parameterName, "Value cannot be null.");
        }
    }

    /// <summary>
    /// Throws an ArgumentException if the provided Guid is empty.
    /// </summary>
    /// <param name="id">The Guid to check.</param>
    /// <param name="parameterName">The name of the Guid parameter being checked (for clearer exception messages).</param>
    public static void ThrowIfEmptyId(Guid id, string parameterName)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Id cannot be empty.", parameterName);
        }
    }

    /// <summary>
    /// Throws an ArgumentNullException if the object is not found (null), typically used in repository or service methods.
    /// </summary>
    /// <param name="obj">The object to check.</param>
    /// <param name="entityName">The name of the entity, for a clearer exception message.</param>
    public static void ThrowIfObjectNotFound(object obj, string entityName)
    {
        if (obj == null)
        {
            throw new ArgumentNullException(entityName, $"{entityName} data not found.");
        }
    }

    /// <summary>
    /// Throws an InvalidOperationException if the provided condition is not met.
    /// </summary>
    /// <param name="condition">The condition to check.</param>
    /// <param name="message">The message to throw in case the condition fails.</param>
    public static void ThrowIfInvalidOperation(bool condition, string message)
    {
        if (!condition)
        {
            throw new InvalidOperationException(message);
        }
    }
}
