import '../styles/lots.scss';

function Lots() {
    return (
        <div>
            <ul className='lots-list'>
            <div className="lot-card">
                <img className="lot-card__image" src={'https://res.cloudinary.com/dev6imkrv/image/upload/v1652985837/OnlineAuction/Lots/29c0bbdc-1684-4934-9596-7d4037631c53/3026ea8a-852e-4f48-be60-6e3cd09af911.jpg'}/>
                <div className='lot-card__data'>
                    <div className='data__property'>
                        <p><b>Creator:</b></p>
                        <p>Цукров Даниил Дмитриевич</p>
                    </div>
                    <div className='data__property'>
                        <p><b>Description :</b></p>
                        <p>Б/У ноутбук, ему 3 года, внутри стоит видеокарта GTX 1050TI, 8gb ddr4 RAM,
                            Intel I5-5500H.
                        </p>
                        <p>
                            Отлично подходит для работы и игр.
                        </p>
                    </div>
                    <p><b>Status:</b> <span className='data__status_not-submited'> Not Submited</span></p>
                    <div className='lot-card__publish-button-block'>
                        {/* <button className='lot-card__publish-button'>Set to Auction</button> */}
                        <button className='lot-card__submit-button'>Submit</button>
                    </div>
                </div>
            </div>      
        </ul>

            <div className='lots-create-dialog'>
                    <div className='dialog__header'>
                        <p>Lot Creation</p>
                        <p className='header__caution'>To post lot to auction you will need to wait for moderation approve</p>
                    </div>
                    
                    <div className='dialog__inputs'>
                        <textarea className='inputs__text-area' placeholder='Write Description here'></textarea>
                        <input type={'file'}/>
                        <button className='add-img__button'>+</button>
                    </div>
                    <button className='dialog__button'>Submit Creation</button>
            </div>

            <div className='auctions-create-dialog'>
                    <div className='dialog__header'>
                        <p>Auction Creation</p>
                    </div>
                    
                    <div className='dialog__inputs'>
                        <select>
                            <section>Opened</section>
                            <section>Closed</section>
                        </select>
                    </div>
                    <button className='dialog__button'>Submit Creation</button>
            </div>
        </div>
      
    );
}
  
export default Lots;