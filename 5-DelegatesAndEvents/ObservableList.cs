namespace DelegatesAndEvents
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <inheritdoc cref="IObservableList{T}" />
    public class ObservableList<TItem> : IObservableList<TItem>
    {
        private readonly IList<TItem> elements = new List<TItem>();

        /// <inheritdoc cref="IObservableList{T}.ElementInserted" />
        public event ListChangeCallback<TItem> ElementInserted;

        /// <inheritdoc cref="IObservableList{T}.ElementRemoved" />
        public event ListChangeCallback<TItem> ElementRemoved;

        /// <inheritdoc cref="IObservableList{T}.ElementChanged" />
        public event ListElementChangeCallback<TItem> ElementChanged;

        /// <inheritdoc cref="ICollection{T}.Count" />
        public int Count
        {
            get => this.elements.Count;
        }

        /// <inheritdoc cref="ICollection{T}.IsReadOnly" />
        public bool IsReadOnly
        {
            get => this.elements.IsReadOnly;
        }

        /// <inheritdoc cref="IList{T}.this" />
        public TItem this[int index]
        {
            get => this.elements[index];
            set
            {
                TItem oldValue = this.elements[index];
                this.elements[index] = value;
                this.ElementChanged?.Invoke(this, value, oldValue, index);
            }
        }

        /// <inheritdoc cref="IEnumerable{T}.GetEnumerator" />
        public IEnumerator<TItem> GetEnumerator()
        {
            return this.elements.GetEnumerator();
        }

        /// <inheritdoc cref="IEnumerable.GetEnumerator" />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <inheritdoc cref="ICollection{T}.Add" />
        public void Add(TItem item)
        {
            this.elements.Add(item);
            this.ElementInserted?.Invoke(this, item, this.elements.Count - 1);
        }

        /// <inheritdoc cref="ICollection{T}.Clear" />
        public void Clear()
        {
            List<TItem> copy = new List<TItem>(this.elements);
            foreach (TItem element in copy)
            {
                Remove(element);
            }
        }

        /// <inheritdoc cref="ICollection{T}.Contains" />
        public bool Contains(TItem item)
        {
            return this.elements.Contains(item);
        }

        /// <inheritdoc cref="ICollection{T}.CopyTo" />
        public void CopyTo(TItem[] array, int arrayIndex) => this.elements.CopyTo(array, arrayIndex);

        /// <inheritdoc cref="ICollection{T}.Remove" />
        public bool Remove(TItem item)
        {
            int indexToRemove = this.elements.IndexOf(item);

            if (indexToRemove >= 0)
            {
                TItem itemToRemove = this.elements[indexToRemove];
                this.elements.RemoveAt(indexToRemove);
                this.ElementRemoved?.Invoke(this, itemToRemove, indexToRemove);

                return true;
            }

            return false;
        }

        /// <inheritdoc cref="IList{T}.IndexOf" />
        public int IndexOf(TItem item)
        {
            return this.elements.IndexOf(item);
        }

        /// <inheritdoc cref="IList{T}.RemoveAt" />
        public void Insert(int index, TItem item)
        {
            this.elements.Insert(index, item);
            this.ElementInserted?.Invoke(this, item, index);
        }

        /// <inheritdoc cref="IList{T}.RemoveAt" />
        public void RemoveAt(int index)
        {
            var removedItem = this.elements[index];
            this.elements.RemoveAt(index);
            this.ElementRemoved?.Invoke(this, removedItem, index);
        }

        public bool Equals(ObservableList<TItem> other)
        {
            return this.elements.Equals(other.elements);
        }

        /// <inheritdoc cref="object.Equals(object?)" />
        public override bool Equals(object obj)
        {
            // TODO improve
            if (this == obj)
            {
                return true;
            }
            if (obj is null || obj.GetType() != this.GetType())
            {
                return false;
            }
            return this.Equals(obj as ObservableList<TItem>);
        }

        /// <inheritdoc cref="object.GetHashCode" />
        public override int GetHashCode()
        {
            // TODO improve
            return this.elements.GetHashCode();
        }

        /// <inheritdoc cref="object.ToString" />
        public override string ToString()
        {
            // TODO improve
            return "[" + string.Join(", ", this.elements) + "]";
        }
    }
}
