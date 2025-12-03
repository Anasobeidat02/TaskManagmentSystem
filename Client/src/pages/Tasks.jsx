import { useState, useEffect } from "react";
import api from "../api/axios";
import { toast } from "react-toastify";

const Tasks = () => {
  const [tasks, setTasks] = useState([]);
  const [loading, setLoading] = useState(true);
  const [showModal, setShowModal] = useState(false);
  const [currentTask, setCurrentTask] = useState(null);

  const [formData, setFormData] = useState({
    title: "",
    description: "",
    status: 1,
    priority: 1,
    dueDate: "",
  });

  const fetchTasks = async () => {
    try {
      const response = await api.get("/api/TaskItems/get-all");
      setTasks(response.data);
    } catch (error) {
      console.error(error);
      toast.error("Failed to load tasks");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchTasks();
  }, []);

  const handleDelete = async (id) => {
    if (window.confirm("Are you sure you want to delete this task?")) {
      try {
        await api.delete(`/api/TaskItems/delete/${id}`);
        toast.success("Task deleted");
        fetchTasks();
      } catch (error) {
        console.error(error);
        toast.error("Failed to delete task");
      }
    }
  };

  const getStatusId = (statusName) => {
    const statuses = {
      Pending: 1,
      "In Progress": 2,
      Completed: 3,
      "On Hold": 4,
      Cancelled: 5,
    };
    return statuses[statusName] || 1;
  };

  const getPriorityId = (priorityName) => {
    const priorities = { Low: 1, Medium: 2, High: 3, Urgent: 4 };
    return priorities[priorityName] || 1;
  };

  const openModal = (task = null) => {
    if (task) {
      setCurrentTask(task);
      setFormData({
        title: task.title,
        description: task.description || "",
        status: getStatusId(task.status),
        priority: getPriorityId(task.priority),
        dueDate: task.dueDate ? task.dueDate.split("T")[0] : "",
      });
    } else {
      setCurrentTask(null);
      setFormData({
        title: "",
        description: "",
        status: 1,
        priority: 1,
        dueDate: "",
      });
    }
    setShowModal(true);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const payload = {
        ...formData,
        status: parseInt(formData.status),
        priority: parseInt(formData.priority),
        dueDate: formData.dueDate
          ? new Date(formData.dueDate).toISOString()
          : null,
      };

      if (currentTask) {
        await api.put("/api/TaskItems/update", {
          ...payload,
          id: currentTask.id,
        });
        toast.success("Task updated");
      } else {
        await api.post("/api/TaskItems/create", payload);
        toast.success("Task created");
      }
      setShowModal(false);
      fetchTasks();
    } catch (error) {
      console.error(error);
      toast.error("Operation failed");
    }
  };

  const getStatusName = (status) => {
    // Status is now a string from backend
    return status || "Unknown";
  };

  const getPriorityName = (priority) => {
    // Priority is now a string from backend
    return priority || "None";
  };

  return (
    <div className="min-h-screen bg-slate-50 p-8">
      <div className="max-w-7xl mx-auto">
        <div className="flex justify-between items-center mb-8">
          <div>
            <h1 className="text-4xl font-bold text-slate-800">My Tasks</h1>
            <p className="text-slate-500 mt-2">Manage your tasks efficiently</p>
          </div>
          <button
            onClick={() => openModal()}
            className="bg-indigo-600 text-white px-6 py-3 rounded-lg hover:bg-indigo-700 active:bg-indigo-800 transition duration-200 shadow-lg hover:shadow-indigo-500/30 font-semibold flex items-center gap-2"
          >
            <svg
              xmlns="http://www.w3.org/2000/svg"
              className="h-5 w-5"
              viewBox="0 0 20 20"
              fill="currentColor"
            >
              <path
                fillRule="evenodd"
                d="M10 3a1 1 0 011 1v5h5a1 1 0 110 2h-5v5a1 1 0 11-2 0v-5H4a1 1 0 110-2h5V4a1 1 0 011-1z"
                clipRule="evenodd"
              />
            </svg>
            Add New Task
          </button>
        </div>

        {loading ? (
          <div className="flex justify-center items-center h-64">
            <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-indigo-600"></div>
          </div>
        ) : (
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
            {tasks.map((task) => (
              <div
                key={task.id}
                className="bg-white p-6 rounded-xl shadow-sm hover:shadow-md transition-shadow duration-200 border border-slate-100 flex flex-col"
              >
                <div className="flex justify-between items-start mb-4">
                  <h3 className="text-xl font-bold text-slate-800 line-clamp-1">
                    {task.title}
                  </h3>
                  <span
                    className={`text-xs font-semibold px-2.5 py-0.5 rounded-full ${
                      task.status === "Completed"
                        ? "bg-emerald-100 text-emerald-800"
                        : task.status === "In Progress"
                        ? "bg-blue-100 text-blue-800"
                        : task.status === "Cancelled"
                        ? "bg-red-100 text-red-800"
                        : "bg-amber-100 text-amber-800"
                    }`}
                  >
                    {getStatusName(task.status)}
                  </span>
                </div>
                <p className="text-slate-600 mb-6 flex-grow line-clamp-3">
                  {task.description}
                </p>
                <div className="border-t border-slate-100 pt-4 mt-auto">
                  <div className="flex justify-between items-center text-sm text-slate-500 mb-4">
                    <div className="flex items-center gap-1">
                      <span
                        className={`w-2 h-2 rounded-full ${
                          task.priority === "Urgent"
                            ? "bg-red-500"
                            : task.priority === "High"
                            ? "bg-orange-500"
                            : task.priority === "Medium"
                            ? "bg-blue-500"
                            : "bg-slate-400"
                        }`}
                      ></span>
                      {getPriorityName(task.priority)} Priority
                    </div>
                    <div>
                      {task.dueDate
                        ? new Date(task.dueDate).toLocaleDateString()
                        : "No date"}
                    </div>
                  </div>
                  <div className="flex justify-between items-center">
                    <div className="text-sm font-medium text-slate-500 flex items-center gap-2">
                      <div className="w-6 h-6 rounded-full bg-indigo-100 text-indigo-600 flex items-center justify-center text-xs">
                        {task.userName
                          ? task.userName.charAt(0).toUpperCase()
                          : "U"}
                      </div>
                      {task.userName || "Unknown User"}
                    </div>
                    <div className="flex space-x-3">
                      <button
                        onClick={() => openModal(task)}
                        className="text-indigo-600 hover:text-indigo-800 font-medium text-sm transition-colors"
                      >
                        Edit
                      </button>
                      <button
                        onClick={() => handleDelete(task.id)}
                        className="text-rose-500 hover:text-rose-700 font-medium text-sm transition-colors"
                      >
                        Delete
                      </button>
                    </div>
                  </div>
                </div>
              </div>
            ))}
          </div>
        )}

        {showModal && (
          <div className="fixed inset-0 bg-slate-900/50 backdrop-blur-sm flex items-center justify-center p-4 z-50">
            <div className="bg-white rounded-2xl p-8 w-full max-w-lg shadow-2xl transform transition-all">
              <div className="flex justify-between items-center mb-6">
                <h2 className="text-2xl font-bold text-slate-800">
                  {currentTask ? "Edit Task" : "New Task"}
                </h2>
                <button
                  onClick={() => setShowModal(false)}
                  className="text-slate-400 hover:text-slate-600"
                >
                  <svg
                    xmlns="http://www.w3.org/2000/svg"
                    className="h-6 w-6"
                    fill="none"
                    viewBox="0 0 24 24"
                    stroke="currentColor"
                  >
                    <path
                      strokeLinecap="round"
                      strokeLinejoin="round"
                      strokeWidth={2}
                      d="M6 18L18 6M6 6l12 12"
                    />
                  </svg>
                </button>
              </div>
              <form onSubmit={handleSubmit} className="space-y-4">
                <div>
                  <label className="block text-slate-700 text-sm font-semibold mb-2">
                    Title
                  </label>
                  <input
                    type="text"
                    className="w-full px-4 py-2 rounded-lg border border-slate-300 focus:border-indigo-500 focus:ring-2 focus:ring-indigo-200 transition duration-200 outline-none"
                    value={formData.title}
                    onChange={(e) =>
                      setFormData({ ...formData, title: e.target.value })
                    }
                    required
                    placeholder="Task title"
                  />
                </div>
                <div>
                  <label className="block text-slate-700 text-sm font-semibold mb-2">
                    Description
                  </label>
                  <textarea
                    className="w-full px-4 py-2 rounded-lg border border-slate-300 focus:border-indigo-500 focus:ring-2 focus:ring-indigo-200 transition duration-200 outline-none"
                    rows="3"
                    value={formData.description}
                    onChange={(e) =>
                      setFormData({ ...formData, description: e.target.value })
                    }
                    placeholder="Task description"
                  />
                </div>
                <div className="grid grid-cols-2 gap-4">
                  <div>
                    <label className="block text-slate-700 text-sm font-semibold mb-2">
                      Status
                    </label>
                    <select
                      className="w-full px-4 py-2 rounded-lg border border-slate-300 focus:border-indigo-500 focus:ring-2 focus:ring-indigo-200 transition duration-200 outline-none bg-white"
                      value={formData.status}
                      onChange={(e) =>
                        setFormData({ ...formData, status: e.target.value })
                      }
                    >
                      <option value={1}>Pending</option>
                      <option value={2}>In Progress</option>
                      <option value={3}>Completed</option>
                      <option value={4}>On Hold</option>
                      <option value={5}>Cancelled</option>
                    </select>
                  </div>
                  <div>
                    <label className="block text-slate-700 text-sm font-semibold mb-2">
                      Priority
                    </label>
                    <select
                      className="w-full px-4 py-2 rounded-lg border border-slate-300 focus:border-indigo-500 focus:ring-2 focus:ring-indigo-200 transition duration-200 outline-none bg-white"
                      value={formData.priority}
                      onChange={(e) =>
                        setFormData({ ...formData, priority: e.target.value })
                      }
                    >
                      <option value={1}>Low</option>
                      <option value={2}>Medium</option>
                      <option value={3}>High</option>
                      <option value={4}>Urgent</option>
                    </select>
                  </div>
                </div>
                <div>
                  <label className="block text-slate-700 text-sm font-semibold mb-2">
                    Due Date
                  </label>
                  <input
                    type="date"
                    className="w-full px-4 py-2 rounded-lg border border-slate-300 focus:border-indigo-500 focus:ring-2 focus:ring-indigo-200 transition duration-200 outline-none"
                    value={formData.dueDate}
                    onChange={(e) =>
                      setFormData({ ...formData, dueDate: e.target.value })
                    }
                  />
                </div>
                <div className="flex justify-end space-x-3 pt-4">
                  <button
                    type="button"
                    onClick={() => setShowModal(false)}
                    className="px-6 py-2.5 rounded-lg text-slate-700 hover:bg-slate-100 font-medium transition-colors"
                  >
                    Cancel
                  </button>
                  <button
                    type="submit"
                    className="px-6 py-2.5 bg-indigo-600 text-white rounded-lg hover:bg-indigo-700 active:bg-indigo-800 font-medium shadow-lg hover:shadow-indigo-500/30 transition-all"
                  >
                    Save Changes
                  </button>
                </div>
              </form>
            </div>
          </div>
        )}
      </div>
    </div>
  );
};

export default Tasks;
