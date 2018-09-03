using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HorseScrapping
{
    public class FileWriter
    {
        private string filepath = string.Empty;
        private StreamWriter sw = null;
        public FileWriter(string _filepath)
        {
            filepath = _filepath;
        }
        private ReaderWriterLockSlim lock_ = new ReaderWriterLockSlim();
        public bool WriteData(string writestr, string path)
        {
            bool result = true;
            lock_.EnterWriteLock();
            try
            {
                if (!File.Exists(path))
                {
                    sw = new StreamWriter(filepath, true, Encoding.UTF8);
                    sw.WriteLine(writestr);
                    sw.Close();

                }

            }
            catch
            {
                result = false;
            }
            finally
            {
                lock_.ExitWriteLock();
            }
            return result;
        }
        public List<string> readRow()
        {
            var arr = new List<string>();
            //lock_.ExitReadLock();
            try
            {
                using (var stream = new StreamReader(filepath))
                {
                    while (!stream.EndOfStream)
                    {
                        var splits = stream.ReadLine().Split('\t');
                        arr.Add(splits[0]);

                    }

                }

            }
            catch
            {
            }
            finally
            {
                //lock_.ExitWriteLock();
            }
            return arr;
        }

        public bool WriteRow(string writestr)
        {
            bool result = true;
            lock_.EnterWriteLock();
            try
            {
                sw = new StreamWriter(filepath, true, Encoding.UTF8);
                sw.WriteLine(writestr);
                sw.Close();

            }
            catch
            {
                result = false;
            }
            finally
            {
                lock_.ExitWriteLock();
            }
            return result;
        }
    }
}
