using System;
using System.Threading.Tasks;

namespace CommonLibrary4Net
{
    public static class FuncExt
    {
        public static Task<TResult> AsTask<TResult>(this Func<TResult> func)
        {
            var tcs = new TaskCompletionSource<TResult>();
            func.BeginInvoke(iar =>
            {
                try
                {
                    tcs.TrySetResult(func.EndInvoke(iar));
                }
                catch (OperationCanceledException)
                {
                    tcs.TrySetCanceled();
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex);
                }
            }, null);

            return tcs.Task;
        }

        public static Task<TResult> AsTask<T1, TResult>(this Func<T1, TResult> func, T1 arg1)
        {
            var tcs = new TaskCompletionSource<TResult>();
            func.BeginInvoke(arg1, iar =>
            {
                try
                {
                    tcs.TrySetResult(func.EndInvoke(iar));
                }
                catch (OperationCanceledException)
                {
                    tcs.TrySetCanceled();
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex);
                }
            }, null);

            return tcs.Task;
        }

        public static Task<TResult> AsTask<T1, T2, TResult>(this Func<T1, T2, TResult> func, T1 arg1, T2 arg2)
        {
            var tcs = new TaskCompletionSource<TResult>();
            func.BeginInvoke(arg1, arg2, iar =>
            {
                try
                {
                    tcs.TrySetResult(func.EndInvoke(iar));
                }
                catch (OperationCanceledException)
                {
                    tcs.TrySetCanceled();
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex);
                }
            }, null);

            return tcs.Task;
        }

        public static Task<TResult> AsTask<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> func, T1 arg1, T2 arg2, T3 arg3)
        {
            var tcs = new TaskCompletionSource<TResult>();
            func.BeginInvoke(arg1, arg2, arg3, iar =>
            {
                try
                {
                    tcs.TrySetResult(func.EndInvoke(iar));
                }
                catch (OperationCanceledException)
                {
                    tcs.TrySetCanceled();
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex);
                }
            }, null);

            return tcs.Task;
        }

        public static Task<TResult> AsTask<T1, T2, T3, T4, TResult>(
            this Func<T1, T2, T3, T4, TResult> func, 
            T1 arg1, 
            T2 arg2, 
            T3 arg3,
            T4 arg4)
        {
            var tcs = new TaskCompletionSource<TResult>();
            func.BeginInvoke(arg1, arg2, arg3, arg4, iar =>
            {
                try
                {
                    tcs.TrySetResult(func.EndInvoke(iar));
                }
                catch (OperationCanceledException)
                {
                    tcs.TrySetCanceled();
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex);
                }
            }, null);

            return tcs.Task;
        }

        public static Task<TResult> AsTask<T1, T2, T3, T4, T5, TResult>(
            this Func<T1, T2, T3, T4, T5, TResult> func,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5)
        {
            var tcs = new TaskCompletionSource<TResult>();
            func.BeginInvoke(arg1, arg2, arg3, arg4, arg5, iar =>
            {
                try
                {
                    tcs.TrySetResult(func.EndInvoke(iar));
                }
                catch (OperationCanceledException)
                {
                    tcs.TrySetCanceled();
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex);
                }
            }, null);

            return tcs.Task;
        }

        public static Task<TResult> AsTask<T1, T2, T3, T4, T5, T6, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, TResult> func,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6)
        {
            var tcs = new TaskCompletionSource<TResult>();
            func.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, iar =>
            {
                try
                {
                    tcs.TrySetResult(func.EndInvoke(iar));
                }
                catch (OperationCanceledException)
                {
                    tcs.TrySetCanceled();
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex);
                }
            }, null);

            return tcs.Task;
        }

        public static Task<TResult> AsTask<T1, T2, T3, T4, T5, T6, T7, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, TResult> func,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7)
        {
            var tcs = new TaskCompletionSource<TResult>();
            func.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, iar =>
            {
                try
                {
                    tcs.TrySetResult(func.EndInvoke(iar));
                }
                catch (OperationCanceledException)
                {
                    tcs.TrySetCanceled();
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex);
                }
            }, null);

            return tcs.Task;
        }

        public static Task<TResult> AsTask<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8)
        {
            var tcs = new TaskCompletionSource<TResult>();
            func.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, iar =>
            {
                try
                {
                    tcs.TrySetResult(func.EndInvoke(iar));
                }
                catch (OperationCanceledException)
                {
                    tcs.TrySetCanceled();
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex);
                }
            }, null);

            return tcs.Task;
        }

        public static Task<TResult> AsTask<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8,
            T9 arg9)
        {
            var tcs = new TaskCompletionSource<TResult>();
            func.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, iar =>
            {
                try
                {
                    tcs.TrySetResult(func.EndInvoke(iar));
                }
                catch (OperationCanceledException)
                {
                    tcs.TrySetCanceled();
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex);
                }
            }, null);

            return tcs.Task;
        }

        public static Task<TResult> AsTask<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> func,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8,
            T9 arg9,
            T10 arg10)
        {
            var tcs = new TaskCompletionSource<TResult>();
            func.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, iar =>
            {
                try
                {
                    tcs.TrySetResult(func.EndInvoke(iar));
                }
                catch (OperationCanceledException)
                {
                    tcs.TrySetCanceled();
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex);
                }
            }, null);

            return tcs.Task;
        }

        public static Task<TResult> AsTask<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> func,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8,
            T9 arg9,
            T10 arg10,
            T11 arg11)
        {
            var tcs = new TaskCompletionSource<TResult>();
            func.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, iar =>
            {
                try
                {
                    tcs.TrySetResult(func.EndInvoke(iar));
                }
                catch (OperationCanceledException)
                {
                    tcs.TrySetCanceled();
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex);
                }
            }, null);

            return tcs.Task;
        }

        public static Task<TResult> AsTask<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> func,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8,
            T9 arg9,
            T10 arg10,
            T11 arg11,
            T12 arg12)
        {
            var tcs = new TaskCompletionSource<TResult>();
            func.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, iar =>
            {
                try
                {
                    tcs.TrySetResult(func.EndInvoke(iar));
                }
                catch (OperationCanceledException)
                {
                    tcs.TrySetCanceled();
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex);
                }
            }, null);

            return tcs.Task;
        }

        public static Task<TResult> AsTask<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> func,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8,
            T9 arg9,
            T10 arg10,
            T11 arg11,
            T12 arg12,
            T13 arg13)
        {
            var tcs = new TaskCompletionSource<TResult>();
            func.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, iar =>
            {
                try
                {
                    tcs.TrySetResult(func.EndInvoke(iar));
                }
                catch (OperationCanceledException)
                {
                    tcs.TrySetCanceled();
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex);
                }
            }, null);

            return tcs.Task;
        }

        public static Task<TResult> AsTask<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> func,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8,
            T9 arg9,
            T10 arg10,
            T11 arg11,
            T12 arg12,
            T13 arg13,
            T14 arg14)
        {
            var tcs = new TaskCompletionSource<TResult>();
            func.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, iar =>
            {
                try
                {
                    tcs.TrySetResult(func.EndInvoke(iar));
                }
                catch (OperationCanceledException)
                {
                    tcs.TrySetCanceled();
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex);
                }
            }, null);

            return tcs.Task;
        }

        public static Task<TResult> AsTask<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> func,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8,
            T9 arg9,
            T10 arg10,
            T11 arg11,
            T12 arg12,
            T13 arg13,
            T14 arg14,
            T15 arg15)
        {
            var tcs = new TaskCompletionSource<TResult>();
            func.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, iar =>
            {
                try
                {
                    tcs.TrySetResult(func.EndInvoke(iar));
                }
                catch (OperationCanceledException)
                {
                    tcs.TrySetCanceled();
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex);
                }
            }, null);

            return tcs.Task;
        }

        public static Task<TResult> AsTask<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> func,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8,
            T9 arg9,
            T10 arg10,
            T11 arg11,
            T12 arg12,
            T13 arg13,
            T14 arg14,
            T15 arg15,
            T16 arg16)
        {
            var tcs = new TaskCompletionSource<TResult>();
            func.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, iar =>
            {
                try
                {
                    tcs.TrySetResult(func.EndInvoke(iar));
                }
                catch (OperationCanceledException)
                {
                    tcs.TrySetCanceled();
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex);
                }
            }, null);

            return tcs.Task;
        }
    }
}
