
namespace IntegerIndexedPriorityQueue
{
	public class HeapItem
	{
		/// <summary>
		/// Any ordinal typed object gets or sets the priority of the heap item (lower value = higher priority).
		/// </summary>
		public object Priority { get; set; }
		/// <summary>
		/// Gets or sets the index of the heap item in the heap items array.
		/// </summary>
		public int index { get; set; }

		/// <summary>
		/// Initializes a new instance of the HeapItem class.
		/// </summary>
		/// <param name="priority">Initial priority of this item</param>	
		public HeapItem(object priority)
		{
			Priority = priority;
			index = -1;
		}
	}

	/// <summary>
	/// Minimum (or maximum depending on the <see cref="_comparison"> comparison method) priority queue
	/// binary heap width optimized "decrease-key" and "increase-key" operations
	/// based on TIPQ class by Rassim Eminli
	/// (http://c-evo.org/files/download.php?remi.remi.zip)
	/// </summary>
	internal class IndexedPriorityQueue
	{
		/// <summary>
		/// Current size of the heap.
		/// </summary>
		protected int _n;
		/// <summary>
		/// Maximum size of the heap.
		/// </summary>
		protected int _max;
		/// <summary>
		/// Array to store the heap items.
		/// </summary>
		protected HeapItem[] _heap;
		/// <summary>
		/// Comparison function to determine the priority of heap items.
		/// </summary>
		private readonly Comparison<object> _comparison;
		/// <summary>
		/// Gets the current size of the heap.
		/// </summary>
		public int n { get { return _n; } }
		/// <summary>
		/// Gets the maximum size of the heap.
		/// </summary>
		public int max { get { return _max; } }

		/// <summary>
		/// Initializes a new instance of the IndexedPriorityQueue class.
		/// </summary>
		/// <param name="comparison">Comparison function to determine the priority of heap items.</param>
		/// <param name="max">Maximum size of the heap.</param>
		public IndexedPriorityQueue(Comparison<object> comparison, int max = 64)
		{
			if (max <= 0)
				max = 64;
			_max = max;
			_heap = new HeapItem[_max];
			_comparison = comparison;
		}

		/// <summary>
		/// Initializes a new instance of the IndexedPriorityQueue class by copying another instance.
		/// </summary>
		/// <param name="other">The IndexedPriorityQueue instance to copy.</param>
		public IndexedPriorityQueue(IndexedPriorityQueue other)
		{
			_max = other._max;
			_n = other._n;
			_heap = new HeapItem[_max];
			Array.Copy(other._heap, _heap, _n);
			_comparison = other._comparison;
		}

		public void Clear()
		{
			Array.Clear(_heap, 0, _heap.Length);
			_n = 0;
		}

		public int Count { get { return _n; } }

		public HeapItem? this[int i]
		{
			get
			{
				if (i < 0 || i >= _n)
					return default;

				return _heap[i];
			}
		}

		public bool Add(HeapItem item, object priority)
		{
			int i = item.index;

			if (i >= 0)
			{
				if (_comparison(_heap[i].Priority, priority) <= 0)
				{
					item.Priority = priority;
					return IncreaseKey(item);
				}
			}
			else
				i = _n++;

			if (_n >= _max)
			{
				_max += _max;
				Array.Resize(ref _heap, _max);
			}

			while (i > 0)
			{
				int j = (i - 1) >> 1;
				if (_comparison(item.Priority, _heap[j].Priority) < 0)
				{
					_heap[i] = _heap[j];
					_heap[i].index = i;
					i = j;
				}
				else
					break;
			}

			// Correct the index of the item in the heap array
			item.index = i;
			//	Set the priority of the item to the new value.
			item.Priority = priority;
			//	Insert the new Item at the insertion point found.
			_heap[i] = item;
			return true;
		}

		public bool IncreaseKey(HeapItem item)
		{
			int i = item.index, t = _n - 1;
			int j = i + 1;
			while (j <= t)
			{
				//	Right(i) = Left(i) + 1
				if (j < t)
					if (_comparison(_heap[j].Priority, _heap[j + 1].Priority) > 0)
						j++;

				HeapItem jVal = _heap[j];

				if (_comparison(jVal.Priority, item.Priority) > 0)
					break;

				_heap[i] = jVal;
				_heap[i].index = i;

				i = j;
				j = (i << 1) + 1;
			}

			// Insert the last in the correct place in the heap.
			_heap[i] = item;
			_heap[i].index = i;


			return true;
		}
		//Left(i) = 2 * i + 1.	
		//Right(i) = 2 * i + 2 => Left(i) + 1
		public bool Get(out HeapItem? item)
		{
			item = default;
			if (_n == 0)
				return false;

			item = _heap[0];
			_n--;

			if (_n > 0)
			{
				HeapItem last = _heap[_n];

				int i = 0, j = 1, t = _n - 1;

				while (j <= t)
				{
					//	Right(i) = Left(i) + 1
					if (j < t)
						if (_comparison(_heap[j].Priority, _heap[j + 1].Priority) > 0)
							j++;

					HeapItem jVal = _heap[j];

					if (_comparison(jVal.Priority, last.Priority) > 0)
						break;

					_heap[i] = jVal;
					_heap[i].index = i;

					i = j;
					j = (j << 1) + 1;
				}

				// Insert the last in the correct place in the heap.
				_heap[i] = last;
				_heap[i].index = i;

			}
			item.index = -1;
			return true;
		}

		public bool Peek(out HeapItem Value)
		{
			Value = default;
			if (_n == 0)
				return false;

			Value = _heap[0];
			return true;
		}

		/// <summary>
		/// Add Synonyms
		/// </summary>
		public bool Put(HeapItem item, object priority)
		{
			return Add(item, priority);
		}

		public bool Push(HeapItem item, object priority)
		{
			return Add(item, priority);
		}

		public bool Insert(HeapItem item, object priority)
		{
			return Add(item, priority);
		}

		/// <summary>
		/// Get Synonyms
		/// Extracts the minimum or maximum value from
		/// the priority queue, depending on the comparison function.
		/// </summary>

		public bool Pop(out HeapItem? item)
		{
			return Get(out item);
		}

		//public bool ExtractMax(out HeapItem Value)
		public bool ExtractMin(out HeapItem? item)
		{
			return Get(out item);
		}
		/// <summary>
		/// Peek Synonym
		/// </summary>
		public bool Top(out HeapItem? item)
		{
			item = default;
			if (_n == 0)
				return false;

			item = _heap[0];
			return true;
		}
	}
}

