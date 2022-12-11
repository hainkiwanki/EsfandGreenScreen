using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T m_instance;

    public static T Inst
    {
        get
        {
            // First check
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<T>();
            }
            // Second check
            if(m_instance == null)
            {
                var singletonObject = new GameObject();
                m_instance = singletonObject.AddComponent<T>();
                singletonObject.name = typeof(T).ToString();
            }
            return m_instance;
        }
    }
}
