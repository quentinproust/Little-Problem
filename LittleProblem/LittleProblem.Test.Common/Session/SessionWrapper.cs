using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using LittleProblem.Common.Colllections;

namespace LittleProblem.Test.Common.Session
{
    public class SessionWrapper : HttpSessionStateBase
    {
        private readonly IDictionary<string, object> _internal = new Dictionary<string, object>();

        public override HttpSessionStateBase Contents
        {
            get { return this; }
        }
        public override bool IsNewSession
        {
            get { return true; }
        }
        public override bool IsReadOnly
        {
            get { return false; }
        }
        public override NameObjectCollectionBase.KeysCollection Keys
        {
            get
            {
                var nameValueCollection = new NameValueCollection(_internal.Count);
                _internal.ForEach(x => nameValueCollection.Add(x.Key, x.Key));

                var keys = (NameObjectCollectionBase.KeysCollection) 
                    Activator.CreateInstance(typeof (NameObjectCollectionBase.KeysCollection), nameValueCollection);
                return keys;
            }
        }

        public override object this[int index]
        {
            get { return _internal.Values.Take(index).First();}
            set
            {
                var key = _internal.Keys.Take(index).First(); 

                if (!String.IsNullOrEmpty(key)) _internal[key] = value;
                else return;
            }
        }
        public override object this[string name]
        {
            get { return _internal[name]; }
            set { _internal[name] = value; }
        }
        public override int Count
        {
            get { return _internal.Count; }
        }
        public override bool IsSynchronized
        {
            get { return false; }
        }
        
        public override void Abandon()
        {
            _internal.Clear();
        }
        public override void Add(string name, object value)
        {
            _internal[name] = value;
        }
        public override void Clear()
        {
            _internal.Clear();
        }
        public override void Remove(string name)
        {
            _internal.Remove(name);
        }
        public override void RemoveAll()
        {
            _internal.Clear();
        }
        public override void RemoveAt(int index)
        {
            var key = _internal.Keys.Take(index).First();
            _internal.Remove(key);
        }
        
        public override IEnumerator GetEnumerator()
        {
            return _internal.GetEnumerator();
        }
    }
}
