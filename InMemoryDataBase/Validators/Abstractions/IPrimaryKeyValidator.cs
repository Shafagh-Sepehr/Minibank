﻿using System.Reflection;

namespace InMemoryDataBase.Validators.Abstractions;

public interface IPrimaryKeyValidator
{
    void Validate<T>(T entity, PropertyInfo[] properties,IReadOnlyDictionary<string, List<object>> entities);
}
