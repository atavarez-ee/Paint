/******************************************************************************
 * Copyright (C) Leap Motion, Inc. 2011-2018.                                 *
 * Leap Motion proprietary and  confidential.                                 *
 *                                                                            *
 * Use subject to the terms of the Leap Motion SDK Agreement available at     *
 * https://developer.leapmotion.com/sdk_agreement, or another agreement       *
 * between Leap Motion and you, your company or other organization.           *
 ******************************************************************************/

 namespace Leap.Unity {

  public class RingBuffer<T> {

    private T[] arr;
    private int firstIdx = 0;
    private int lastIdx = -1;

    public RingBuffer(int bufferSize) {
      arr = new T[bufferSize];
    }

    public int Count {
      get {
        if (lastIdx == -1) return 0;
        int diff = (lastIdx + 1) - firstIdx;
        if (diff <= 0) return diff + arr.Length;
        return diff;
      }
    }

    public int Capacity {
      get { return arr.Length; }
    }

    public bool IsFull {
      get { return Count == Capacity; }
    }

    public void Clear() {
      lastIdx = -1;
      firstIdx = 0;
    }

    public void Add(T t) {
      if (IsFull) {
        firstIdx += 1;
        firstIdx %= arr.Length;
      }
      lastIdx += 1;
      lastIdx %= arr.Length;

      arr[lastIdx] = t;
    }

    /// <summary>
    /// Oldest element is at index 0, youngest is at Length - 1.
    /// </summary>
    public T Get(int idx) {
      return arr[(firstIdx + idx) % arr.Length];
    }

    public T GetLatest() {
      return Get(Count - 1);
    }

    public T GetOldest() {
      return Get(0);
    }

    public void Set(int idx, T t) {
      int actualIdx = (firstIdx + idx) % arr.Length;
      arr[actualIdx] = t;
    }

    public void SetLatest(T t) {
      Set(Count - 1, t);
    }

  }

}
