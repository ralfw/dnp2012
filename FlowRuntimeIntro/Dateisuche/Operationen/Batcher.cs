using System;
using System.Collections.Generic;
using System.Linq;


namespace Dateisuche.Operationen
{
    class Batch<T>
    {
        public Batch(T[] elements, string correlationId) { Elements = elements; CorrelationId = correlationId; }

        public string CorrelationId { get; private set; }
        public T[] Elements { get; private set; }

        public bool IsEmpty { get { return Elements.Length == 0; } }

        public void ForEach(Action<T> execute)
        {
            Elements.ToList().ForEach(execute);
        }
    }

    class EndOfStreamBatch<T> : Batch<T>
    {
        public EndOfStreamBatch(T[] elements, string correlationId) : base(elements, correlationId) {}
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


        public Batch<T> Grab() { return Grab(""); } 
        public Batch<T> Grab(string correlationId)
        {
            var batch = new Batch<T>(_elements.ToArray(), correlationId);
            _elements = new List<T>();
            return batch;
        }


        public EndOfStreamBatch<T> GrabAsEndOfStream() { return GrabAsEndOfStream(""); }
        public EndOfStreamBatch<T> GrabAsEndOfStream(string correlationId)
        {
            var batch = new EndOfStreamBatch<T>(_elements.ToArray(), correlationId);
            _elements = new List<T>();
            return batch;
        }
    }
}