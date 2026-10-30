namespace IntegerIndexedPriorityQueue
{
	/// <summary>
	/// Represents a task with a name and associated data.
	/// </summary>
	class Task : HeapNode
	{
		/// <summary>
		/// Gets or sets the data associated with the task.
		/// </summary>
		public object Data { get; set; }
		/// <summary>
		/// Gets or sets the name of the task.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Initializes a new instance of the Task class with the specified name and priority.
		/// </summary>
		/// <param name="name">The name of the task.</param>
		/// <param name="priority">The priority of the task by default(lower value = higher priority).</param>
		/// <see cref="HeapNode.Data">The Data property can be set to any object you want to associate with this task.</see>
		public Task(string name, object priority) : base(priority)
		{
			Data = // You can set the Data property to any object you want to associate with this task
				Name = name;
			Priority = priority;
		}
		/// <summary>
		/// Initializes a new instance of the Task class by copying another instance.
		/// </summary>
		/// <param name="other">The IndexedPriorityQueue instance to copy.</param>
		public Task(Task other) : base(other.Priority)
		{
			Data = other.Data;
			Name = other.Name;
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
}
