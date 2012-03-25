using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using Raven.Client.Document;

namespace Homesite.App.Providers.Storage
{
    public class StorageClient : IStorageClient
    {
        readonly Lazy<DocumentStore> _docStore = new Lazy<DocumentStore>(() =>
        {
            var documentStore = new DocumentStore();
            var connectionString = ConfigurationManager.AppSettings["RAVENHQ_CONNECTION_STRING"];
            documentStore.ParseConnectionString(connectionString);
            documentStore.Initialize();
            return documentStore;
        }, LazyThreadSafetyMode.PublicationOnly);
        
        protected DocumentStore DocStore
        {
            get { return _docStore.Value; }
        }
        
        public void Store(object doc)
        {
            using(var session = DocStore.OpenSession())
            {
                session.Store(doc);
                session.SaveChanges();
            }
        }

        public IList<T> Query<T>()
        {
            try
            {
                using (var session = DocStore.OpenSession())
                {
                    return session.Query<T>().ToList();
                }
            }
            // In case of There is no index named: dynamic/SubmissionDocs 
            // let's return empty collection... Stupid situation.
            catch (InvalidOperationException)
            {                
                return new List<T>();
            }            
        }
    }
}