using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeTime;
using System.Collections;

    class PlanneryEnum : IEnumerable
{
    PlanneryDay[] _planneryDay;
    public PlanneryEnum(PlanneryDay[] pArray)
        {
        _planneryDay = new PlanneryDay[pArray.Length];
        for (int i = 0; i < pArray.Length; i++)
            {
                _planneryDay[i] = pArray[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public PlanneryDayEnum GetEnumerator()
        {
            return new PlanneryDayEnum(_planneryDay);
        }
    }
    class PlanneryDayEnum :IEnumerator
{
            PlanneryDay[] _planneryDay;

         
            int position = -1;

         public PlanneryDayEnum(PlanneryDay[] list)
            {
          _planneryDay = list;
            }

            public bool MoveNext()
            {
                position++;
                return (position < _planneryDay.Length);
            }

            public void Reset()
            {
                position = -1;
            }

    object IEnumerator.Current => Current;

   public  PlanneryDay Current
            {
                get
                {
                    try
                    {
                        return _planneryDay[position];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
        }

    

