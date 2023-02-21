import React from 'react';
import { TodoContext } from '../ToDoContext';
import './TodoForm.css';

function TodoForm() {
  // Creamos un estado para nuestro nuevo TODO
  const [newTaskDescription, setNewTaskDescription] = React.useState('');
  // Desestructuramos las funciones que necesitamos para añadir un TODO y cerrar nuestro modal
  const { isOpenModal, setIsOpenModal, createTask} = React.useContext(TodoContext);
  
  
  const onChange = (event) => {
    setNewTaskDescription(event.target.value);
  };
  
  const onCancel = () => {
    setIsOpenModal(false);
  };
  
  // Función para agregar nuestro nuevo TODO
  const onSubmit = (event) => {
    // prevent default para evitar recargar la página
    event.preventDefault();
    // Utilizamos nuestra función para añadir nuestro TODO
    createTask({description: newTaskDescription, ToDoListId:1});
    // Cerramos nustro modal
    setIsOpenModal(false);
    // También estaría bien resetear nuestro formulario
    setNewTaskDescription('')
  };

  return (
    <form onSubmit={onSubmit}>
      <label className='modal-title'>Agregar Tarea</label>
      <textarea value={newTaskDescription}
        onChange={onChange}
        minLength = '0'
        maxLength = '200'
        placeholder="Agrega la descripción de la tarea"
      />
      <div className="TodoForm-buttonContainer">
        <button type="button"  className="TodoForm-button TodoForm-button--cancel" onClick={onCancel}>
          Cancelar
        </button>
        <button disabled={!newTaskDescription.length > 0} type="submit" className="TodoForm-button TodoForm-button--add">
          Añadir
        </button>
      </div>
    </form>
  );
}

export { TodoForm };