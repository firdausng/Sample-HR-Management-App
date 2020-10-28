using System;
using System.Collections.Generic;
using System.Text;

namespace App.Exceptions
{
    public class EntityNotFoundException : Exception, IAppException
    {
        public EntityNotFoundException(string name, object key)
            : base($"Entity '{name}' ({key}) was not found.")
        {
        }

    }

    public class EntityNotAssignedException : Exception, IAppException
    {
        public EntityNotAssignedException(string name, object key, string parentName)
            : base($"Entity '{name}' ({key}) was not assigned to {parentName}")
        {
        }

    }

    public class RelatedEntityNotFoundException : Exception, IAppException
    {
        public RelatedEntityNotFoundException(string name, object key)
            : base($"Entity '{name}' related to {key} was not found.")
        {
        }

    }

    public class EntityAlreadyExistException : Exception, IAppException
    {
        public EntityAlreadyExistException(string name, object key)
            : base($"Entity '{name}' ({key}) already exist")
        {
        }
    }
}
