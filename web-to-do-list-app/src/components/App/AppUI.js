import React from 'react';

import { TodoCounter } from "../TodoCounter";
import { TodoSearch } from "../TodoSearch";
import { TodoList } from "../TodoList";
import { TodoTask } from "../TodoTask";
import { CreateTodoButtom } from "../CreateTodoButtom";
import { TodoContext } from '../ToDoContext';
import { LoadingSpinner } from '../LoadingSpinner'
import { Modal } from '../Modal';
import {TodoForm} from '../TodoForm'

function AppUI() {
    const {error, loading, filteredTask, onCompletedTaskChange, deleteTodo, isOpenModal} = React.useContext(TodoContext);

    return (
        <React.Fragment >
          <div className="todo-list">
            {loading && <LoadingSpinner/>}
            <TodoCounter />
            <TodoSearch />
            <TodoList>
              {error && <LoadingSpinner/>}
              
              {(!loading && !filteredTask.length) && <p>¡Crea tu primera tarea!</p>}
              
              {filteredTask.map(task => (
                <TodoTask
                  key={task.id}
                  description={task.description}
                  isCompleted={task.isCompleted}
                  onCompletedTaskChange={() => onCompletedTaskChange(task)}
                  onDeleteTask={() => deleteTodo(task.id)}
                />
              ))}
            </TodoList>  

            {!!isOpenModal && (
              <Modal>
                <TodoForm></TodoForm>
              </Modal>
            )}            
          </div>    
          <CreateTodoButtom/>       
        </React.Fragment>
      );
}

export { AppUI };
