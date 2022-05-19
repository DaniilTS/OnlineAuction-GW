import { create } from "../../../services/lotCategoryService.js";

function LotCategoriesTable(props) {
 
    async function createLotCategory() {
        const name = document.getElementById('lot-category-name-input').value;
        create(name).then(val => {
            props.updatelotCategoriesTable();
        });
    }

    const lotCategoriesRows = props.lotCategories?.map((lc, index) => {
        return (
            <tr className='lot-categories__row' key={lc.id} >
                <td className='row-index'>{index + 1}</td>
                <td className='row-property'>{lc.name}</td>
                <td className='row-actions'>
                    <button className='actions__button button-delete'>Delete</button>
                </td>
            </tr>
        );
    });

    return (
        <>
            <div className="action-block">
                <input id={'lot-category-name-input'} className="block__input" type={'text'} placeholder={'Category name'}/>
                <button className="block__button" onClick={() => createLotCategory()}>Add</button>
            </div>
            <table className='lot-categories__table' cellSpacing={0} cellPadding={0}>
                <thead className='table__head'>
                    <tr>
                        <th className='table__cell'>№</th>
                        <th className='table__cell'>Name</th>
                        <th className='table__cell'>Actions</th>
                    </tr>
                </thead>
                <tbody className='table__body'>
                    {lotCategoriesRows}
                </tbody>
            </table>
        </>
        
    );
}
  
export default LotCategoriesTable;