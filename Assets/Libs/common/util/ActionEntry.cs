using System;

namespace cambrian.common
{
    public class ActionEntry<T>
    {
        Action<T, Exception> callback;

        T value;

        Exception exception;

        public ActionEntry(Action<T, Exception> callback)
        {
            this.callback = callback;
        }
        public void setValue(T value, Exception exception = null)
        {
            this.value = value;
            this.exception = exception;
        }
        public void excute()
        {
            if(this.value==null) return;
            T value=this.value;
            Exception exception=this.exception;
            this.value = default(T);
            this.exception = null;
            this.callback(value, exception);
        }
    }
}