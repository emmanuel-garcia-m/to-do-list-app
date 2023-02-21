import React from "react";
import './CreateTodoButtom.css'
import { TodoContext } from '../ToDoContext';

function CreateTodoButtom(){

    const { isOpenModal, setIsOpenModal } = React.useContext(TodoContext);

    const onClickCreateButton = (event) => {
        setIsOpenModal(!isOpenModal);
    }

    return(
        <button 
            className="CreateTodoButton"
            onClick={onClickCreateButton}>+</button>
    );
}

export { CreateTodoButtom};