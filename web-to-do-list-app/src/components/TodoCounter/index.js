import React from "react";
import './TodoCounter.css';
import { TodoContext } from '../ToDoContext';

function TodoCounter(){ 
    const { total, completed } = React.useContext(TodoContext);

    return(
        <React.Fragment>
            <h2 className="TodoCounter">
                Has complentado {completed} de {total} tareas.
            </h2>            
        </React.Fragment>       
    )
}

export {TodoCounter};