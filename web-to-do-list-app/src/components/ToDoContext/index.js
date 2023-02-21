import React from 'react';
import { useServiceStorage } from './useServiceStorage';
import Swal from 'sweetalert2'

const TodoContext = React.createContext();

function TodoProvider(props) {
  const {item : taskList, setItem: setTaksList, updateTask , deleteTask, createNewTask,loading, error} = useServiceStorage([]);
  const [searchValue, setSearchValue] = React.useState('');
  const [isOpenModal, setIsOpenModal] = React.useState(false);

  const completed = taskList.filter(todo => !!todo.isCompleted).length;
  const total = taskList.length;

  let filteredTask = [];

  if (!searchValue.length >= 1) {
    filteredTask = taskList;
  } else {
    filteredTask = taskList.filter(todo => {
      const taskDescription = todo.description.toLowerCase();
      const searchText = searchValue.toLowerCase();
      return taskDescription.includes(searchText);
    });
  }

  const onCompletedTaskChange = (taskToUpdate) => {
    const taskIndex = taskList.findIndex(task => task.id === taskToUpdate.id);
    const takslListCpy = [...taskList];
    takslListCpy[taskIndex].completed = true;    
    taskToUpdate.isCompleted = !taskToUpdate.isCompleted;
    updateTask(taskToUpdate);
    setTaksList(takslListCpy);
    Toast.fire({
      icon: 'success',
      title: 'Actualizado correctamente'
    });  
    
  };

  const deleteTodo = (id) => {
    const taskIndex = taskList.findIndex(task => task.id === id);
    const takslListCpy = [...taskList];
    takslListCpy.splice(taskIndex, 1);
    deleteTask(id)
    setTaksList(takslListCpy);
    Toast.fire({
      icon: 'success',
      title: 'Eliminado correctamente' 
    })
  };

  const createTask  = (taskToCreate) => {
    taskToCreate.isCompleted = false;
    const takslListCpy = [...taskList];
    takslListCpy.push(taskToCreate);
    createNewTask(taskToCreate);
    setTaksList(takslListCpy);
    Toast.fire({
      icon: 'success',
      title: 'Se ha creado la tarea correctamente'
    });  
  }

  const Toast = Swal.mixin({
    toast: true,
    position: 'top-end',
    showConfirmButton: false,
    timer: 3000,
    timerProgressBar: true,
    didOpen: (toast) => {
      toast.addEventListener('mouseenter', Swal.stopTimer)
      toast.addEventListener('mouseleave', Swal.resumeTimer)
    }
  })

  if(error){
    Toast.fire({
      icon: 'error',
      title: 'Error consumiendo servicio'
    })
  }
  
  return (
    <TodoContext.Provider value={{loading, error, total, completed, searchValue, setSearchValue, filteredTask, onCompletedTaskChange, deleteTodo, isOpenModal, setIsOpenModal, createTask  }}>
      {props.children}
    </TodoContext.Provider>
  );
}

export { TodoContext, TodoProvider };
