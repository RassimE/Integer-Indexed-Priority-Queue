
namespace IntegerIndexedPriorityQueue
{
	public class HeapItem
	{
		virtual public object Data { get; set; }
		public int index { get; set; }
		public HeapItem(object item = null)
		{
			Data = item;
			index = -1;
		}
	}

	/// <summary>
	/// Minimum priority queue
	/// binary heap width optimized "decrease-key" and "increase-key" operations
	/// based on TIPQ class by Rassim Eminli
	/// (http://c-evo.org/files/download.php?remi.remi.zip)
	/// </summary>
	internal class IndexedPriorityQueue
	{
		protected int _n;                       // current size of the heap.
		protected int _max;                     // max. size of the heap.
		protected HeapItem[] _heap;            // Item of the heap.
		private readonly Comparison<HeapItem> _comparison;
		
		public int n { get { return _n; } }
		public int max { get { return _max; } }

		public IndexedPriorityQueue(Comparison<HeapItem> comparison, int max = 64)
		{
			if (max <= 0)
				max = 64;
			_max = max;
			_heap = new HeapItem[_max];
			_comparison = comparison;
		}

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

		public HeapItem this[int i]
		{
			get
			{
				if (i < 0 || i >= _n)
					return default;

				return _heap[i];
			}
		}

		public bool Add(HeapItem Value)
		{
			int i = Value.index;
			if (i >= 0)
			{
				if (_comparison(_heap[i], Value) <= 0)
					return IncreaseKey(Value);
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
				if (_comparison(Value, _heap[j]) < 0)
				{
					_heap[i] = _heap[j];
					_heap[i].index = i;
					i = j;
				}
				else
					break;
			}

			//	Insert the new Item at the insertion point found.
			Value.index = i;
			_heap[i] = Value;
			return true;
		}

		public bool IncreaseKey(HeapItem Value)
		{
			int i = Value.index, t = _n - 1;
			int j = i + 1;
			while (j <= t)
			{
				//	Right(i) = Left(i) + 1
				if (j < t)
					if (_comparison(_heap[j], _heap[j + 1]) > 0)
						j++;

				HeapItem jVal = _heap[j];

				if (_comparison(jVal, Value) > 0)
					break;

				_heap[i] = jVal;
				_heap[i].index = i;

				i = j;
				j = (i << 1) + 1;
			}

			// Insert the last in the correct place in the heap.
			_heap[i] = Value;
			_heap[i].index = i;


			return true;
		}
		//Left(i) = 2 * i + 1.
		//Right(i) = 2 * i + 2 => Left(i) + 1
		public bool Get(out HeapItem Value)
		{
			Value = default;
			if (_n == 0)
				return false;

			Value = _heap[0];
			_n--;

			if (_n > 0)
			{
				HeapItem last = _heap[_n];

				int i = 0, j = 1, t = _n - 1;

				while (j <= t)
				{
					//	Right(i) = Left(i) + 1
					if (j < t)
						if (_comparison(_heap[j], _heap[j + 1]) > 0)
							j++;

					HeapItem jVal = _heap[j];

					if (_comparison(jVal, last) > 0)
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
			Value.index = -1;
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
		public bool Put(HeapItem Value)
		{
			return Add(Value);
		}

		public bool Push(HeapItem Value)
		{
			return Add(Value);
		}

		public bool Insert(HeapItem Value)
		{
			return Add(Value);
		}

		/// <summary>
		/// Get Synonyms
		/// Extracts the minimum or maximum value from
		/// the priority queue, depending on the comparison function.
		/// </summary>

		public bool Pop(out HeapItem Value)
		{
			return Get(out Value);
		}

		//public bool ExtractMax(out HeapItem Value)
		public bool ExtractMin(out HeapItem Value)
		{
			return Get(out Value);
		}
		/// <summary>
		/// Peek Synonym
		/// </summary>
		public bool Top(out HeapItem Value)
		{
			Value = default;
			if (_n == 0)
				return false;

			Value = _heap[0];
			return true;
		}
	}
}

