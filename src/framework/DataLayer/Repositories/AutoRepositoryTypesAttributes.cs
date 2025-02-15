﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Orion.Framework.DataLayer.Repositories
{
    /// <summary>
    ///     Used to define auto-repository types for entities.
    ///     This can be used for DbContext types.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class AutoRepositoryTypesAttribute : Attribute
    {
        public AutoRepositoryTypesAttribute(
            Type repositoryInterface,
            Type repositoryInterfaceWithPrimaryKey,
            Type repositoryImplementation,
            Type repositoryImplementationWithPrimaryKey)
        {
            RepositoryInterface = repositoryInterface;
            RepositoryInterfaceWithPrimaryKey = repositoryInterfaceWithPrimaryKey;
            RepositoryImplementation = repositoryImplementation;
            RepositoryImplementationWithPrimaryKey = repositoryImplementationWithPrimaryKey;
        }

        public Type RepositoryInterface { get; private set; }

        public Type RepositoryInterfaceWithPrimaryKey { get; private set; }

        public Type RepositoryImplementation { get; private set; }

        public Type RepositoryImplementationWithPrimaryKey { get; private set; }
    }
}
