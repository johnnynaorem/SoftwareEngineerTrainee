import { reactive } from "vue";

export const toastService = reactive({
  toasts: [],
  showToast(message, type = "info", title = "", duration = 5000) {
    const id = Date.now(); // Unique ID for each toast
    this.toasts.push({
      id,
      message,
      type,
      title,
      duration,
      autohide: true,
    });

    // Remove toast after duration
    setTimeout(() => {
      this.removeToast(id);
    }, duration);
  },
  removeToast(id) {
    this.toasts = this.toasts.filter((toast) => toast.id !== id);
  },
});
