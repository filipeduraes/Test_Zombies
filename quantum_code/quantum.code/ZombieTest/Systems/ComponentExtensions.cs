namespace Quantum.ZombieTest.Systems
{
    public static unsafe class ComponentExtensions
    {
        public static bool TryAddComponent<T>(this Frame frame, EntityRef entity, out T* value) where T : unmanaged, IComponent
        {
            value = null;
            
            AddResult result = frame.Add<T>(entity);
            bool wasSuccessful = result is AddResult.ComponentAdded or AddResult.ComponentAlreadyExists;
            
            return wasSuccessful && frame.Unsafe.TryGetPointer(entity, out value);
        }
    }
}