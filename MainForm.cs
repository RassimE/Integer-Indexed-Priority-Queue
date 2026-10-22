using System;
using System.Windows.Forms;

namespace IntegerIndexedPriorityQueue
{
	public partial class MainForm : Form
	{
		/// <summary>
		/// Represents a task with a name and priority.
		/// </summary>
		class Task : HeapItem
		{
			/// <summary>
			/// Gets or sets the data associated with the task.
			/// </summary>
			public override object Data { get; set; }
			/// <summary>
			/// Gets or sets the name of the task.
			/// </summary>
			public string Name { get; set; }
			/// <summary>
			/// Gets or sets the priority of the task (lower value = higher priority).
			/// </summary>
			public int Priority { get; set; }

			/// <summary>
			/// Initializes a new instance of the Task class with the specified name and priority.
			/// </summary>
			/// <param name="name">The name of the task.</param>
			/// <param name="priority">The priority of the task by default(lower value = higher priority).</param>
			/// <see cref="HeapItem.Data">The Data property can be set to any object you want to associate with this task.</see>
			public Task(string name, int priority = int.MaxValue) : base()
			{
				Data = // You can set the Data property to any object you want to associate with this task
					Name = name;
				Priority = priority;
			}

			public Task(Task other) : base()
			{
				Data = other.Data;
				Name = other.Name;
				Priority = other.Priority;
			}
			/// <summary>
			/// Returns a string representation of the task.
			/// </summary>
			/// <returns>A string that represents the task.</returns>
			public override string ToString()
			{
				return $"{Name} (Priority: {Priority})";
			}
		}

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
			// and their priorities
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
				aiTaskQueue.Push(Tasks[i]);
			}

			//Display
			DisplayString($"Peacetime");
			DisplayQueue(aiTaskQueue);
		}

		IndexedPriorityQueue CreateHeapClone(IndexedPriorityQueue heap)
		{
			IndexedPriorityQueue clone = new IndexedPriorityQueue(heap);
			clone.Clear();
			for (int i = 0; i < heap.n; i++)
				clone.Add(new Task((Task)heap[i]));
			return clone;
		}

		static int comparison(HeapItem x, HeapItem y)
		{
			return ((Task)x).Priority.CompareTo(((Task)y).Priority);
		}

		void DisplayString(string text)
		{
			DisplayListBox.Items.Add(text);
		}

		void DisplayQueue(IndexedPriorityQueue Queue)
		{
			IndexedPriorityQueue clone = CreateHeapClone(Queue);

			DisplayString($"Tasks in the queue: {clone.Count}");
			// Extract tasks in order of priority
			DisplayString("\nAI performs tasks in the following order:");
			while (clone.Count > 0)
			{
				clone.Pop(out HeapItem currentTask);
				DisplayString($"- In progress: {currentTask}");
			}
		}

		void ChangeTaskPriorities(int n, int[] newPriorities)
		{
			if (n > Tasks.Length)
				throw new ArgumentException("The number of new priorities must be less than or equal to the number of tasks.");

			for (int i = 0; i < n; i++)
			{
				Tasks[i].Priority = newPriorities[i];
				aiTaskQueue.Push(Tasks[i]);
			}
		}

		private void Situation_CheckedChanged(object sender, EventArgs e)
		{
			if (!((RadioButton)sender).Checked)
				return;
			DisplayListBox.Items.Clear();

			if (radioButton2.Checked)//Wartime
			{
				// SITUATION: The AI's city is under attack by enemies!
				DisplayString($"Wartime");
				// In a standard queue, we would have to iterate through all the elements.
				// Here, however, we can instantly change the priority of any element to the highest (0) in O(log n) time.
				ChangeTaskPriorities(4, WartimePriorities);
			}
			else if (radioButton1.Checked)//Peacetime
			{
				// SITUATION: War ended.
				DisplayString($"Peacetime");
				// In a standard queue, we would have to iterate through all the elements.
				// Here, however, we can instantly change the priority of any element to the highest (0) in O(log n) time.
				ChangeTaskPriorities(4, PeacetimePriorities);
			}

			DisplayQueue(aiTaskQueue);
		}
	}
}
