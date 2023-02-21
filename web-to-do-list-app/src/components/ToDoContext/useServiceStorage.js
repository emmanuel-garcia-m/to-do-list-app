import React from 'react';

function useServiceStorage(initialValue) {
  const [error, setError] = React.useState(false);
  const [loading, setLoading] = React.useState(true);
  const [item, setItem] = React.useState(initialValue);
  
  React.useEffect(() => {
    getTaskList();
  },[]);

  const getTaskList = () => {
    fetch("http://localhost:34709/api/ToDoTask/1")
    .then((respose) => respose.json())
    .then((data) => {
      setItem(data);
      setLoading(false); 
    })
    .catch(() => {
      setLoading(false); 
      setError(true);      
    });
  }
  
  const createNewTask = (taskToCreate) => {
    fetch("http://localhost:34709/api/ToDoTask",
      {method:'POST',
       body: JSON.stringify(taskToCreate),
       headers:{
        'Content-Type': 'application/json'
      }})    
    .then((data) => {      
      setLoading(false); 
    })
    .catch(() => {
      setLoading(false); 
      setError(true);      
    });
  };

  const updateTask = (taskToUpdate) => {
    fetch("http://localhost:34709/api/ToDoTask",
      {method:'PUT',
       body: JSON.stringify(taskToUpdate),
       headers:{
        'Content-Type': 'application/json'
      }})    
    .then((data) => {      
      setLoading(false); 
    })
    .catch(() => {
      setLoading(false); 
      setError(true);      
    });
  }

  const deleteTask = (idToDelete) => {
    fetch(`http://localhost:34709/api/ToDoTask/${idToDelete}`,
      {method:'DELETE'})    
    .then((data) => {      
      setLoading(false); 
    })
    .catch(() => {
      setLoading(false); 
      setError(true);      
    });
  }

  return {item, setItem, getTaskList, createNewTask, updateTask, deleteTask, loading, error};
}

export { useServiceStorage};
