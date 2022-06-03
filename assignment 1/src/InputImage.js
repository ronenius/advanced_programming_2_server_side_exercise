import { useRef, useState } from 'react';
import './InputImage.css'

function Image({ result, setResult, srcRef }) {
    const [image, setImage] = useState(null);
    const imageRef = useRef(null);

    function useDisplayImage(setResult) {
        function uploader(e) {
            const imageFile = e.target.files[0];
            const reader = new FileReader();
            reader.addEventListener("load", (e) => {
                setResult(e.target.result);
            });
            reader.readAsDataURL(imageFile);
        }
        return uploader;
    }

    const uploader = useDisplayImage(setResult);

    function dontReload(e) {
        e.preventDefault();
    }

    return (
        <div className="container">
            <div className="row" id="inputPicture">
                {result && <div className="col">
                    <button id="modalButton" height="100px" width="100px" onClick={dontReload} data-bs-toggle="modal" data-bs-target="#exampleModal">
                        <img ref={imageRef} className="rounded-circle" height="100px" width="100px" style={{ objectFit: "cover" }} src={result} alt="profile_picture" />
                    </button>
                    <div className="modal fade" id="exampleModal" tabIndex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div className="modal-dialog">
                            <div className="modal-content">
                                <img ref={imageRef} height="80%" src={result} alt="profile_picture" />
                            </div>
                        </div>
                    </div>
                </div>}
                <div className="col">
                    <label htmlFor="formFile" className="form-label">Pick profile picture</label>
                    <input className="form-control"
                        type="file"
                        accept="image/*"
                        ref={srcRef}
                        onChange={(e) => {
                            e.preventDefault();
                            setImage(e.target.files[0]);
                            uploader(e);
                        }}
                        id="formFile"
                        placeholder="Choose profile picture"
                    >
                    </input>
                </div>
            </div>
        </div>
    );
}

export default Image;