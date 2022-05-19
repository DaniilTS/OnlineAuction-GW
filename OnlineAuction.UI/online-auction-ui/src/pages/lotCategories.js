import { useEffect, useState } from 'react';
import LotCategoriesTable from '../components/lots/categories/lotCategoriesTable.js';
import { getlotCategories } from "../services/lotCategoryService.js";
import '../styles/lotCategoriesTable.scss';

function LotCategories({ isAuth }) {

    const [ lotCategories, setlotCategories ] = useState(null);
    const [ lotCategoryTable, setlotCategoryTable ] = useState();

    useEffect(() => {
        setlotCategoryTable(<LotCategoriesTable lotCategories={lotCategories} updatelotCategoriesTable={updatelotCategoriesTable}/>);
    }, [lotCategories]);

    function updatelotCategoriesTable(){
        getlotCategories().then(val => {
            setlotCategories(val);
        })
    }
    
    if(!lotCategories && isAuth) {
        updatelotCategoriesTable();
    }

    return (
        <div>
            { lotCategoryTable }
        </div>
    );
}
  
export default LotCategories;