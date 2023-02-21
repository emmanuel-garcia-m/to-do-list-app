import React from 'react';
import './TodoTask.css';


function TodoTask(props) { 
    return (
      <React.Fragment>
      <li className="TodoTask">
        <label >
          <input          
            name={props.id}
            type="checkbox"
            defaultChecked={props.isCompleted}
            onChange={props.onCompletedTaskChange}
          />      
          <div className="task-body" htmlFor={props.Id}>{props.description}       
          </div>       
        </label> 
        <button className="Icon button-delete" onClick = {props.onDeleteTask}>
            x        
        </button>     
      </li> 
      </React.Fragment>     
    );
  }

export { TodoTask };