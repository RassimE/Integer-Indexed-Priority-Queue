namespace IntegerIndexedPriorityQueue
{
	public partial class MainForm : Form
	{
		IndexedPriorityQueue aiTaskQueue;

		// Tasks for the AI's base action plan
		Task CreateFarm;
		Task BuiltCityWall;
		Task BuiltBarracks;
		Task TrainSolders;
		Task ScoutMap;
		Task BuiltHospital;
		Task BuiltRoads;

		// Array of tasks and their priorities for peacetime and wartime
		Task[] Tasks;
		int[] PeacetimePriorities;
		int[] WartimePriorities;
		public MainForm()
		{
			InitializeComponent();

			/// Create an IPQ, where the <param name = "comparison"></param> is specified comparison function and
			/// the Key is the task name (string) and
			/// the Priority is the weight (int; lower value = higher priority)

			aiTaskQueue = new IndexedPriorityQueue(comparison);

			// Create an AI's base action plan with tasks
			// and their default priorities
			CreateFarm = new Task("Build a farm", 4);
			BuiltRoads = new Task("Built a roads", 5);
			BuiltHospital = new Task("Built a hospital", 6);
			BuiltCityWall = new Task("Build a city wall", 7);
			ScoutMap = new Task("Explore the map", 8);
			BuiltBarracks = new Task("Build barracks", 9);
			TrainSolders = new Task("Train a warrior", 11);

			Tasks = new Task[] { BuiltHospital, BuiltRoads, BuiltBarracks, TrainSolders, CreateFarm, BuiltCityWall, ScoutMap };
			PeacetimePriorities = new int[] { 6, 5, 9, 11, 4, 7, 8 };
			WartimePriorities = new int[] { 1, 3, 2, 0, 4, 7, 8 };

			// Populating the AI's base action plan
			for (int i = 0; i < Tasks.Length; i++)
			{
				Tasks[i].Priority = PeacetimePriorities[i];
				aiTaskQueue.Push(Tasks[i], Tasks[i].Priority);
			}

			//Display
			ShowString($"Peacetime");
			ShowQueue(aiTaskQueue);
		}

		IndexedPriorityQueue CreateHeapClone(IndexedPriorityQueue heap)
		{
			IndexedPriorityQueue clone = new IndexedPriorityQueue(heap);
			clone.Clear();
			for (int i = 0; i < heap.n; i++)
				clone.Add(new Task((Task)heap[i]), ((Task)heap[i]).Priority);
			return clone;
		}

		static int comparison(object x, object y)
		{
			return ((int)x).CompareTo((int)y);
		}

		void ShowString(string text)
		{
			DisplayListBox.Items.Add(text);
		}

		void ShowQueue(IndexedPriorityQueue Queue)
		{
			IndexedPriorityQueue clone = CreateHeapClone(Queue);

			ShowString($"Tasks in the queue: {clone.Count}");
			// Extract tasks in order of priority
			ShowString("\nAI performs tasks in the following order:");
			while (clone.Count > 0)
			{
				clone.Pop(out HeapItem currentTask);
				ShowString($"- In progress: {currentTask}");
			}
		}

		void ChangeTaskPriorities(int n, int[] newPriorities)
		{
			if (n > Tasks.Length)
				throw new ArgumentException("The number of new priorities must be less than or equal to the number of tasks.");

			for (int i = 0; i < n; i++)
			{
				//Tasks[i].Priority = newPriorities[i];
				aiTaskQueue.Push(Tasks[i], newPriorities[i]);
			}
		}

		private void Situation_Changed(object sender, EventArgs e)
		{
			if (!((RadioButton)sender).Checked)
				return;
			DisplayListBox.Items.Clear();

			if (rbWartime.Checked)//Wartime
			{
				// SITUATION: The AI's city is under attack by enemies!
				ShowString($"Wartime");
				// In a standard queue, we would have to iterate through all the elements.
				// Here, however, we can instantly change the priority of any element to the highest (0) in O(log n) time.
				ChangeTaskPriorities(4, WartimePriorities);
			}
			else if (rbPeasetime.Checked)//Peacetime
			{
				// SITUATION: War ended.
				ShowString($"Peacetime");
				// In a standard queue, we would have to iterate through all the elements.
				// Here, however, we can instantly change the priority of any element to the highest (0) in O(log n) time.
				ChangeTaskPriorities(4, PeacetimePriorities);
			}

			ShowQueue(aiTaskQueue);
		}
	}
}
