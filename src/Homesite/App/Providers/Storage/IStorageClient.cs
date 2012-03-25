using System.Collections.Generic;

namespace Homesite.App.Providers.Storage
{
    public interface IStorageClient
    {
        void Store(object doc);
        IList<T> Query<T>();
    }
}