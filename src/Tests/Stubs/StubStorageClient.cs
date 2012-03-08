using System.Collections.Generic;
using System.Linq;
using Homesite.App.Providers.Storage;

namespace Tests.Stubs
{
    public class StubStorageClient : IStorageClient
    {
        readonly IList<object> _store = new List<object>();

        public void Store(object doc)
        {
            _store.Add(doc);
        }

        public IList<T> Query<T>()
        {
            return _store.OfType<T>().ToList();
        }
    }
}
