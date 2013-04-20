using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.Linq
{
    public class Table<T> : Collection<T>, ITable, ITable<T> where T : class
    {
        protected override void InsertItem(int index, T item)
        {
            // TODO: Add change listener on new item
            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            // TODO: Remove change listener on old item
            base.RemoveItem(index);
        }

        protected override void SetItem(int index, T item)
        {
            // TODO: Remove change listener on old item
            // TODO: Add change listener on new item
            base.SetItem(index, item);
        }

        public void Attach(T entity)
        {
            Add(entity);
        }

        public void DeleteOnSubmit(T entity)
        {
            Remove(entity);
        }

        public void InsertOnSubmit(T entity)
        {
            Add(entity);
        }

        public DataContext Context
        {
            get; internal set;
        }

        public bool IsReadOnly
        {
            get { return false; }
        }
    }
}
