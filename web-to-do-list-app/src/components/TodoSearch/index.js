import React from "react";
import './TodoSearch.css';
import { TodoContext } from '../ToDoContext';

function TodoSearch(){  
    const { searchValue, setSearchValue } = React.useContext(TodoContext);

    const onSearchValueChange = (event) => {
        setSearchValue(event.target.value)
    }

    return(
        <div className="file-input">
            <input className="text" placeholder="Filtro.." maxLength="25" value={searchValue} onChange={onSearchValueChange}/>
        </div>       
    );
}

export {TodoSearch};