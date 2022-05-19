import '../styles/auctions.scss';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import Users from './users';
import Lots from './lots';
import Auctions from './auctions';

function MainRoutingPage(props) {
    return (
        <>
            <Router>
            <div className='auction-pages-links'>
                <ul className='pages__links'>
                    <Link to="/auctions">Auctions | </Link>
                    <Link to="/lots">Lots | </Link>
                    <Link to="/offers">Offers | </Link>
                    <Link to="/lots/categories">Lot Categories | </Link>
                    <Link to="/users">Users</Link>
                </ul>
            </div>
            <div className='auction-main'>
                <Routes>
                    <Route exact path='/auctions' element={< Auctions {...props} />}></Route>
                    <Route exact path='/users' element={< Users {...props} />}></Route>
                    <Route exact path='/lots' element={< Lots {...props} />}></Route>
                </Routes>
            </div>
            </Router>
        </>
        
    );
}
  
export default MainRoutingPage;