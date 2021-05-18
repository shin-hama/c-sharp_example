using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace utility
{
    class AsyncUtil
    {
        static private Object SyncObject = new Object();
        private List<Object> noOrdered = new List<Object>();
        private List<Object> ordered = new List<Object>();
        public void LockObject()
        {
            Task[] results = new Task[100];
            for (int i = 0; i < 100; i++)
            {
                // 外部変数化しておかないと変数 i の変化とスレッド実行のラグが影響して、追加する値の安全性を保てない
                results[i] = AddList(i);
            }

            Task.WaitAll(results);
            Console.WriteLine(noOrdered.Count);
            Console.WriteLine(string.Join(", ", noOrdered));

            // 順番まで保証するなら for ループもLockステート内に入れる
            var result = Task.Run(() =>
            {
                lock (SyncObject)
                {
                    for (int i = 0; i < 100; i++)
                    {
                        ordered.Add(i);
                    }
                }
            });
            result.Wait();

            Console.WriteLine(ordered.Count);
            Console.WriteLine(string.Join(", ", ordered));
        }

        /// <summary>
        /// グローバルな配列へのアクセスを排他にすることで、すべての要素が加えられる
        /// ことを保証する。ただし、追加される順番は保証されない。実際に書くスレッドが
        /// Lock を取る順番は実行のたびに変わってしまうから
        /// また、この方法では、追加する配列そのものをLockしている。今回は追加するだけ
        /// のサンプルなので問題ないが、仮に配列の要素内のデータをLock する処理がある
        /// 場合、デッドロックに陥る可能性がある。デッドロックを回避するために、静的な
        /// ロック専用のオブジェクトを定義することが推奨されている。
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private Task AddListWithLockValue(int i)
        {
            return Task.Run(() =>
            {
                lock (noOrdered)
                {
                    noOrdered.Add(i);
                }
            });
        }

        /// <summary>
        /// ロック用に専用のオブジェクトを使用したよりスレッドセーフな例
        /// 順番は保証されないが、test 内の要素が安全であることは保証される
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private Task AddList(int i)
        {
            return Task.Run(() =>
            {
                lock (SyncObject)
                {
                    noOrdered.Add(i);
                }
            });
        }
    }
}
