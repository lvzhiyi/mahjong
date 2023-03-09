
namespace cambrian.common
{
    public class History<K, V>
    {
        ArrayList<Track<K, V>> array = new ArrayList<Track<K, V>>();


        public int count
        {
            get { return this.array.count; }
        }
        public V get(int paramInt)
        {
            return this.array.get(paramInt).value;
        }
        public V getLast()
        {
            if (this.count == 0) return default(V);
            return this.get(this.count-1);
        }
        public void add(K key, V value)
        {
            for (int i = 0; i < this.array.count; i++)
            {
                if (this.array.get(i).key.Equals(key))
                    this.array.removeAt(i);
            }
            this.array.add(new Track<K, V>(key, value));
        }
        public V[] toArray()
        {
            V[] arrayOfObject = new V[this.count];
            for (int i = 0; i < arrayOfObject.Length; i++)
            {
                arrayOfObject[i] = this.get(i);
            }
            return arrayOfObject;
        }
    }

    public class Track<K, V>
    {
        public Track(K key, V value)
        {
            this.key = key;
            this.value = value;
        }
        public K key;
        public V value;
    }
}