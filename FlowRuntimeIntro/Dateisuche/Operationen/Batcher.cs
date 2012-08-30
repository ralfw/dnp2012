using System;
using System.Collections.Generic;
using System.Linq;


namespace Dateisuche.Operationen
{
    class Batch<T>
    {
        public Batch(T[] elements) { Elements = elements; }
        public T[] Elements { get; private set; }

        public bool IsEmpty { get { return Elements.Length == 0; } }

        public void ForEach(Action<T> execute)
        {
            Elements.ToList().ForEach(execute);
        }
    }

    public enum BatchStatus
    {
        SpaceLeft,
        Full
    }

    class Batcher<T>
    {
        private readonly int _maxSize;
        private List<T> _elements;

        public Batcher(int maxSize)
        {
            _maxSize = maxSize;
            _elements = new List<T>();
        }


        public BatchStatus Add(T element)
        {
            _elements.Add(element);
            return Status;
        }


        private BatchStatus Status
        {
            get { return _elements.Count >= _maxSize ? BatchStatus.Full : BatchStatus.SpaceLeft; }
        }
        
        
        public Batch<T> Grab()
        {
            var batch = new Batch<T>(_elements.ToArray());
            _elements = new List<T>();
            return batch;
        }

    }
}