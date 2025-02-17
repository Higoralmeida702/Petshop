import '../Home/Home.css'

const Home = () => {
    return (
        <div>
            <div className="cachorroImgBackground"></div>
            <section className='introducaoHome'>
                <div className='introducaoHomeConteudo'>
                    <h1>Projeto acadêmico de sistema ERP completo</h1>
                    <p>Desenvolvido por Higor Almeida</p>
                </div>
            </section>
            <section className='classesAnimaisAtendidos'>
                <div className='classesAnimaisAtendidosGrid'>
                    <div className="imgSvgAnimais">
                        <img src="/public/svg/dog-svgrepo-com.svg" alt="homeImagemCachorro" />
                        <h1>Cachorros</h1>
                    </div>
                    <div className="imgSvgAnimais">
                        <img src="/public/svg/cat-svgrepo-com.svg" alt="homeImagemGato" />
                        <h1>Gatos</h1>
                    </div>
                    <div className="imgSvgAnimais">
                        <img src="/public/svg/reptile-tail-svgrepo-com.svg" alt="homeImagemReptil" />
                        <h1>Répteis</h1>
                    </div>
                    <div className="imgSvgAnimais">
                        <img src="/public/svg/bird-bold-svgrepo-com.svg" alt="" />
                        <h1>Aves</h1>
                    </div>
                </div>
                <a className='btnAvancar'>Avançar para projeto</a>
            </section>

        </div>
    );
}

export default Home
