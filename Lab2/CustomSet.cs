using System.Collections;
using System.Runtime.CompilerServices;

namespace Lab2;

using System.Text;

public class CustomSet<T> : ICollection<T>
{
    private const int DefaultCapacity = 4;

    private T[] _items = new T[DefaultCapacity];
    private int _size = 0;

    public CustomSet()
    {
    }

    public CustomSet(IEnumerable<T> items)
    {
        AddAll(items);
    }

    public int Capacity
    {
        get => _items.Length;
        set
        {
            if (value < _size)
            {
                throw new IndexOutOfRangeException("Index is out of range.");
            }

            if (value != _items.Length)
            {
                if (value > 0)
                {
                    T[] newItems = new T[value];
                    if (_size > 0)
                    {
                        Array.Copy(_items, newItems, _size);
                    }

                    _items = newItems;
                }
                else
                {
                    _items = [];
                }
            }
        }
    }

    public void AddAll(IEnumerable<T> items)
    {
        foreach (var item in items)
        {
            Add(item);
        }
    }

    public int Count => _size;
    public bool IsReadOnly => false;

    private int _version;

    public void Add(T item)
    {
        if (!_items.Contains(item))
        {
            _version++;
            T[] array = _items;
            int size = _size;
            if ((uint)size < (uint)array.Length)
            {
                _size = size + 1;
                array[size] = item;
            }
            else
            {
                AddWithResize(item);
            }
        }
    }

    private void AddWithResize(T item)
    {
        int size = _size;
        Grow(size + 1);
        _size = size + 1;
        _items[size] = item;
    }

    public CustomSet<T> Copy()
    {
        return new CustomSet<T>(_items);
    }

    private void Grow(int capacity)
    {
        int newCapacity = _items.Length == 0 ? DefaultCapacity : 2 * _items.Length;

        if (newCapacity > Array.MaxLength) newCapacity = Array.MaxLength;

        if (newCapacity < capacity) newCapacity = capacity;

        Capacity = newCapacity;
    }


    public int IndexOf(T item)
        => Array.IndexOf(_items, item, 0, _size);

    public void CopyTo(T[] array, int arrayIndex)
    {
        Array.Copy(_items, 0, array, arrayIndex, _size);
    }

    public bool Remove(T item)
    {
        var index = IndexOf(item);
        if (index < 0) return false;
        RemoveAt(index);
        return true;
    }

    public void RemoveAt(int index)
    {
        if (index >= _size)
        {
            throw new IndexOutOfRangeException("Index is out of range.");
        }

        _size--;
        if (index < _size)
        {
            Array.Copy(_items, index + 1, _items, index, _size - index);
        }

        if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
        {
            _items[_size] = default!;
        }

        _version++;
    }

    public void Clear()
    {
        _version++;
        if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
        {
            var size = _size;
            _size = 0;
            if (size > 0)
            {
                Array.Clear(_items, 0, size);
            }
        }
        else
        {
            _size = 0;
        }
    }

    public bool Contains(T item)
    {
        return IndexOf(item) != -1;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new Enumerator(this);
    }

    public override bool Equals(object obj)
    {
        if (obj is CustomSet<T> otherSet)
        {
            if (_size != otherSet.Count)
                return false;

            foreach (var item in _items)
            {
                if (!otherSet._items.Contains(item))
                    return false;
            }

            return true;
        }

        return false;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("{ ");
        for (int i = 0; i < _size; i++)
        {
            sb.Append(_items[i]);
            if (i < _size - 1)
            {
                sb.Append(", ");
            }
        }

        sb.Append(" }");
        return sb.ToString();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public static CustomSet<T> operator +(CustomSet<T> first, T second)
    {
        var newSet = first.Copy();
        newSet.Add(second);
        return newSet;
    }

    public static CustomSet<T> operator +(CustomSet<T> first, CustomSet<T> second)
    {
        var newSet = first.Copy();
        newSet.AddAll(second);
        return newSet;
    }

    public static explicit operator int(CustomSet<T> set) => set.Count;

    public static CustomSet<T> operator *(CustomSet<T> first, CustomSet<T> second)
    {
        var newSet = new CustomSet<T>();
        foreach (var x1 in first)
        {
            if (second.Contains(x1))
            {
                newSet.Add(x1);
            }
        }

        return newSet;
    }

    public static bool operator false(CustomSet<T> set)
    {
        return set._size < 1;
    }

    public static bool operator true(CustomSet<T> set)
    {
        return set._size >= 1;
    }

    private struct Enumerator : IEnumerator<T>
    {
        private readonly CustomSet<T> _set;
        private int _index;
        private readonly int _version;
        private T? _current;

        internal Enumerator(CustomSet<T> set)
        {
            _set = set;
            _index = 0;
            _version = set._version;
            _current = default;
        }

        public void Dispose()
        {
        }


        public bool MoveNext()
        {
            if (_version != _set._version || ((uint)_index >= (uint)_set.Count)) return MoveNextRare();
            _current = _set._items[_index];
            _index++;
            return true;
        }

        private bool MoveNextRare()
        {
            if (_version != _set._version)
            {
                throw new InvalidOperationException("Enumerator operation failed version");
            }

            _index = _set.Count + 1;
            _current = default;
            return false;
        }

        public T Current
        {
            get
            {
                if (_index == 0 || _index == _set.Count + 1)
                {
                    throw new InvalidOperationException("Enumerator operation cant happen");
                }

                return _current!;
            }
        }


        object? IEnumerator.Current => Current;

        void IEnumerator.Reset()
        {
            if (_version != _set._version)
            {
                throw new InvalidOperationException("Enumerator operation failed version");
            }

            _index = 0;
            _current = default;
        }
    }
}