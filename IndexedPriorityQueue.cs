
namespace IntegerIndexedPriorityQueue
{
	public class HeapNode
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
		public HeapNode(object priority)
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
		/// default maximum capacity of an array of nodes
		/// </summary>
		const int defaultMaximumCapacity = 64;
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
		protected HeapNode[] _heap;
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
		public IndexedPriorityQueue(Comparison<object> comparison, int max = defaultMaximumCapacity)
		{
			if (max <= 0)
				max = defaultMaximumCapacity;
			_max = max;
			_heap = new HeapNode[_max];
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
			_heap = new HeapNode[_max];
			Array.Copy(other._heap, _heap, _n);
			_comparison = other._comparison;
		}

		/// <summary>
		/// Clears all items from the priority queue.
		/// </summary>
		public void Clear()
		{
			Array.Clear(_heap, 0, _heap.Length);
			_n = 0;
		}

		/// <summary>
		/// Gets the number of items in the priority queue.
		/// </summary>
		public int Count { get { return _n; } }

		/// <summary>
		/// Gets the heap item at the specified index.
		/// </summary>
		/// <param name="i">The index of the heap item to get.</param>
		/// <returns>The heap item at the specified index, or null if the index is out of range.</returns>
		public HeapNode? this[int i]
		{
			get
			{
				if (i < 0 || i >= _n)
					return default;

				return _heap[i];
			}
		}

		/// <summary>
		/// Adds a heap item to the priority queue with the specified priority.
		/// </summary>
		/// <param name="item">The heap item to add.</param>
		/// <param name="priority">The priority of the heap item.</param>
		/// <returns>True if the item was added successfully; otherwise, false.</returns>
		public bool Add(HeapNode item, object priority)
		{
			int i = item.index;

			if (i >= 0) //Check whether the element is already in the queue;
			{
				//if it is, increase its priority, provided that the new priority is higher(i.e., its value is lower).
				if (_comparison(item.Priority, priority) <= 0)
					return IncreaseKey(item, priority);
				//Otherwise, the item's new value is higher — add it with a new priority as usual.
			}
			else //item is not in queue, then add it
				i = _n++;

			// If the heap is full, resize it to accommodate more items.
			if (_n >= _max)
			{
				_max += _max;
				Array.Resize(ref _heap, _max);
			}

			// Move the new item up the heap until it is in the correct position.
			while (i > 0)
			{
				int j = (i - 1) >> 1;
				if (_comparison(priority, _heap[j].Priority) < 0)
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
		/// <summary>
		/// Increases the priority of a heap item in the priority queue.
		/// </summary>
		/// <param name="item">The heap item whose priority is to be increased.</param>
		/// <param name="priority">The new priority of the heap item.</param>
		/// <returns>True if the priority was increased successfully; otherwise, false.</returns>
		public bool IncreaseKey(HeapNode item, object priority)
		{
			int i = item.index;             // index of the item in the heap array
			int t = _n - 1;                 // index of the last item in the heap array
			int j = i + 1;                  // index of the right child of the item in the heap array

			// Move the item down the heap until it is in the correct position.
			while (j <= t)
			{
				//	Right(i) = Left(i) + 1
				if (j < t)
					if (_comparison(_heap[j].Priority, _heap[j + 1].Priority) > 0)
						j++;

				HeapNode jVal = _heap[j];

				if (_comparison(jVal.Priority, priority) > 0)
					break;

				_heap[i] = jVal;
				_heap[i].index = i;

				i = j;
				j = (i << 1) + 1;
			}

			// Correct the index of the item in the heap array
			item.index = i;
			//	Set the priority of the item to the new value.
			item.Priority = priority;
			//	Insert the new Item at the insertion point found.
			_heap[i] = item;

			return true;
		}
		//Left(i) = 2 * i + 1.	
		//Right(i) = 2 * i + 2 => Left(i) + 1

		/// <summary>
		/// Retrieves the element with the highest(or lowest — depending on the comparison function)
		// priority from a priority queue and removes it.
		/// </summary>
		/// <param name="item">When this method returns, contains the heap item with the highest priority, or null if the priority queue is empty.</param>
		/// <returns>True if a heap item was successfully retrieved and removed; otherwise, false.</returns>
		public bool Get(out HeapNode? item)
		{
			item = default;
			if (_n == 0)
				return false;

			item = _heap[0];
			_n--;

			if (_n > 0)
			{
				HeapNode last = _heap[_n];

				int i = 0, j = 1, t = _n - 1;

				// Move the last item down the heap until it is in the correct position.
				while (j <= t)
				{
					//	Right(i) = Left(i) + 1
					if (j < t)
						if (_comparison(_heap[j].Priority, _heap[j + 1].Priority) > 0)
							j++;

					HeapNode jVal = _heap[j];

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

		/// <summary>
		/// Gets the heap item with the highest priority without removing it from the priority queue.
		/// </summary>
		/// <param name="Value">When this method returns, contains the heap item with the highest priority, or null if the priority queue is empty.</param>
		/// <returns>True if a heap item was successfully retrieved; otherwise, false.	</returns>
		public bool Peek(out HeapNode? Value)
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
		public bool Put(HeapNode item, object priority)
		{
			return Add(item, priority);
		}

		public bool Push(HeapNode item, object priority)
		{
			return Add(item, priority);
		}

		public bool Insert(HeapNode item, object priority)
		{
			return Add(item, priority);
		}

		/// <summary>
		/// Get Synonyms
		/// Extracts the minimum or maximum value from
		/// the priority queue, depending on the comparison function.
		/// </summary>

		public bool Pop(out HeapNode? item)
		{
			return Get(out item);
		}

		//public bool ExtractMax(out HeapItem Value)
		public bool ExtractMin(out HeapNode? item)
		{
			return Get(out item);
		}
		/// <summary>
		/// Peek Synonym
		/// </summary>
		public bool Top(out HeapNode? item)
		{
			item = default;
			if (_n == 0)
				return false;

			item = _heap[0];
			return true;
		}
	}
}

