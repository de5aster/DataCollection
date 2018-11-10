var { Button, FormGroup, FormControl, HelpBlock, ControlLabel, Nav, NavItem, Table } = ReactBootstrap;

var works = [];
var equipments = [];
var apiLoad = "home/api/load";
var apiSave = "home/api/save";
var apiSaveDatabase = "home/api/savedatabase";
var apiGetAll = "home/api/getall";
var apiGetExcel = "home/api/download";
var apiGetContractCount = "home/api/getcontractcount";

class MasterWork extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            value: ""            
        };
    }

    onChange = (e) => {
        var val = e.target.value;
        this.setState({
            value: val
        });
    }
    onChangeValue = () => {
        if ( this.state.value  != null)
        {
           setTimeout(this.props.updateMasterWork(this.state.value, this.props.number), 50);
        }
    }

    render() {
        return (
            <tr style={{ paddingBottom: "5px" }}>
                <td id="tbl-lbl" style={{ paddingRight: "5px", paddingTop: "5px" }}><ControlLabel>Выполненные работы:</ControlLabel> </td>
                <td id="data-label" style={{ paddingTop: "5px" }}>
                    <FormControl
                        type="text"
                        value={this.state.value}
                        onChange={this.onChange}
                        onBlur={this.onChangeValue}
                    ></FormControl></td>
            </tr>
            );
    }
}

class RepairEquipment extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            name: "",
            count: ""
        };
    }
    onChangeNameValue = (e) => {
        this.setState({
            name: e.target.value
        });
    }

    onChangeCountValue = (e) => {
        this.setState({
            count: e.target.value
        });
    }
    onChangeValue = () => {
        setTimeout(this.props.updateRepairEquipment(this.state.name, this.state.count, this.props.number), 50);
    }

    render() {
        return (
            <tr>
                <td id="tbl-lbl" style={{ paddingRight: "5px", paddingTop:"5px" }}>
                    <FormControl
                        type="text"
                        value={this.state.name}
                        onChange={this.onChangeNameValue}
                        onBlur={this.onChangeValue}></FormControl>
                </td>
                <td id="data-label" style={{ paddingTop: "5px" }}  >
                    <FormControl
                        type="number"
                        value={this.state.count}
                        onChange={this.onChangeCountValue}
                        onBlur={this.onChangeValue}></FormControl>
                </td>
            </tr>
        );
    }
}

class HomePage extends React.Component
{
    constructor(props) {
        super(props);
        this.state = {
            addVisible: false,
            loadVisible: false,
            databaseVisible: false,
            activeNav: "0",
            contractCount: "",
            deserializeFile: [{
                contractId: "",
                clientName: "",
                clientAddress: "",
                phoneNumber: "",
                email: "",
                equipment: "",
                breakage: "",
                masterName: "",
                masterPersonnelNumber: "",
                putDate: "",
                performDate: "",
                works: [],
                repairEquipments: []
            }]
        };
    }
    onAddClick = (e) => {
        e.preventDefault();
        this.setState(()=> ({
            addVisible: true,
            loadVisible: false,
            databaseVisible: false
        }));
        this.onGetContractCount();
    }
    onLoadClick = (e) => {
        e.preventDefault();
        this.setState(() => ({
            addVisible: false,
            loadVisible: true,
            databaseVisible: false
        }));
    }
    onDatabaseClick = (e) => {
        e.preventDefault();
        this.setState(() => ({
            addVisible: false,
            loadVisible: false,
            databaseVisible: true
        }));
        this.onGetDatabase();
    }
    navItemSelect = (eventKey, e) => {
        e.preventDefault();
        this.setState({
            activeNav: eventKey
        });
    }

    onGetContractCount = () => {
        fetch(apiGetContractCount)
            .then((res) => {
                if (res.status === 200) {
                    return res.json();
                }
                if (res.status === 400) {
                    this.setState({
                        error: "400"
                    });
                }
                return null;
            }, function () {
                this.setState({
                    error: "Что-то пошло не так. Попробуйте обновить страницу и повторить попытку"

                });
            }).then((data) => {
                this.setState({
                    contractCount: data + 1
                });
            });
    }

    onGetDatabase = () => {
        fetch(apiGetAll)
            .then((res) => {
                    if (res.status === 200) {
                        return res.json();
                    }
                    if (res.status === 400) {
                        this.setState({
                            error: "400"
                        });
                    }
                    return null;
            },
            function () {
                this.setState({
                    error: "Что-то пошло не так. Попробуйте обновить страницу и повторить попытку"

                });
            }).then((data) => {
                this.setState({
                    deserializeFile: data
                });
            });
    }
    render() {
        return (
            <div>
                <h1>Карта технических работ</h1>
                <Nav bsStyle="tabs" activeKey={this.state.activeNav} onSelect={this.navItemSelect}>
                    <NavItem eventKey="1" onClick={this.onAddClick}>
                        Создать новую карту
                    </NavItem>
                    <NavItem eventKey="2" onClick={this.onLoadClick}>
                        Загрузить созданную карту
                    </NavItem>
                    <NavItem eventKey="3" onClick={this.onDatabaseClick}>
                        Загрузить из базы
                    </NavItem>
                </Nav>
                <BtnGroup
                    apiUrlLoad={apiLoad}
                    addVisible={this.state.addVisible}
                    loadVisible = {this.state.loadVisible}
                />
                <DataCollection
                    addVisible={this.state.addVisible}
                    apiUrlSave={apiSave}
                    apiUrlSaveDatabase={apiSaveDatabase}
                    contractCount={this.state.contractCount}
                />
                <DatabasePage
                    databaseVisible={this.state.databaseVisible}
                    deserializeFile={this.state.deserializeFile}
                    apiUrlGetExcel={apiGetExcel}
                />
            </div>
            );
    }
}

class DataCollection extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            numEquipment: 0,
            numWorkList: 0,
            ContractId: 0,
            ClientName: "",
            ClientAddress: "",
            PhoneNumber: "",
            Email: "",
            Equipment: "",
            Breakage: "",
            MasterName: "",
            MasterPersonnelNumber: "",
            PutDate: "",
            PerformDate: "",
            WorkList: [],
            RepairEquipments: [],
            inputList:[],
            materialVisible: false,
            workButtonText: "Добавить работу",
            materialButtonText: "Добавить материал"
        };
    }

    onChangeClientName = (e) => {

        this.setState({
            ClientName: e.target.value
        });
    }
    onChangeClientAdress = (e) => {
        this.setState({
            ClientAddress : e.target.value
        });
    }
    onChangePhoneNumber = (e) => {
        this.setState({
            PhoneNumber : e.target.value
        });
    }
    onChangeEmail = (e) => {
        this.setState({
            Email : e.target.value
        });
    }
    onChangeEquipment = (e) => {
        this.setState({
            Equipment : e.target.value
        });
    }
    onChangeBreakage = (e) => {
        this.setState({
            Breakage : e.target.value
        });
    }
    onChangePutDate = (e) => {
        this.setState({
            PutDate : e.target.value
        });
    }
    onChangeMasterName = (e) => {
        this.setState({
            MasterName : e.target.value
        });
    }
    onChangeMasterPersonnelNumber = (e) => {
        this.setState({
            MasterPersonnelNumber : e.target.value
        });
    }
    onChangePerformDate = (e) => {
        this.setState({
            PerformDate : e.target.value
        });
    }
    
    updateMasterWork = (value, number) => {
        if (value != null)
        {
            works[number] = value;
            this.setState({
                WorkList: works
            });
        }
        console.log(typeof (this.state.WorkList));
    }

    updateRepairEquipment = (name, count, number) => {
        if (name != null && count != null)
        {
            console.log(number);
            equipments[number] = [name, count];
            console.log(equipments);
            this.setState({
                RepairEquipments : equipments
            });
            console.log(typeof (this.state.RepairEquipments));
        }
    }
    onDatabaseSaveClick = () => {
        var data = JSON.stringify({
            "ContractId": this.props.contractCount,
            "ClientName": this.state.ClientName,
            "ClientAddress": this.state.ClientAddress,
            "PhoneNumber": this.state.PhoneNumber,
            "Email": this.state.Email,
            "Equipment": this.state.Equipment,
            "Breakage": this.state.Breakage,
            "MasterName": this.state.MasterName,
            "MasterPersonnelNumber": this.state.MasterPersonnelNumber,
            "PutDate": this.state.PutDate,
            "PerformDate": this.state.PerformDate,
            "WorkList": this.state.WorkList,
            "RepairEquipments": this.state.RepairEquipments
        });
        var xhr = new XMLHttpRequest();
        xhr.open("post", this.props.apiUrlSaveDatabase, true);
        xhr.setRequestHeader("Content-type", "application/json");
        xhr.onload = function() {
            if (xhr.status === 200) {
                alert(xhr.responseText);
            }
            if (xhr.status === 409) {
                alert("ContractId already exists");
            }
        }
        xhr.send(data); 
    }

    onSaveClick = () => {
        
        var data = JSON.stringify({
            "ContractId": this.props.contractCount,
            "ClientName": this.state.ClientName,
            "ClientAddress": this.state.ClientAddress,
            "PhoneNumber": this.state.PhoneNumber,
            "Email": this.state.Email,
            "Equipment": this.state.Equipment,
            "Breakage": this.state.Breakage,
            "MasterName": this.state.MasterName,
            "MasterPersonnelNumber": this.state.MasterPersonnelNumber,
            "PutDate": this.state.PutDate,
            "PerformDate": this.state.PerformDate,
            "WorkList": this.state.WorkList,
            "RepairEquipments": this.state.RepairEquipments
        });
        var xhr = new XMLHttpRequest();
        xhr.open("post", this.props.apiUrlSave, true);
        xhr.setRequestHeader("Content-type", "application/json");
        xhr.onload = function (e) {            
            var blob = xhr.response;
            this.saveOrOpenBlob(blob);
        }.bind(this);
        xhr.send(data);        
    }
    saveOrOpenBlob = (blob) => {
        var data = new Blob([blob], { type: "text/xml" }),        
            fileUrl = window.URL.createObjectURL(data),
            tempLink = document.createElement("a");
        tempLink.href = fileUrl;
        tempLink.setAttribute("download", "client.xml");
        tempLink.click();
    }

    onAddMasterWork = () => {
        this.setState({
            numWorkList: this.state.numWorkList + 1,
            workButtonText: "Добавить ещё"
        });
    }

    onAddRepairEquipments = () => {
        this.setState({
            numEquipment: this.state.numEquipment + 1,
            materialVisible: true,
            materialButtonText: "Добавить ещё"
        });
    }
    render() {
        const workList = [];
        for (var i = 0; i < this.state.numWorkList; i += 1) {
            workList.push(<MasterWork key={i} number={i} updateMasterWork={this.updateMasterWork}/>);
        }
        const equipmentList = [];
        for (var i = 0; i < this.state.numEquipment; i += 1) {
            equipmentList.push(<RepairEquipment key={i} number={i} updateRepairEquipment={this.updateRepairEquipment}/>);
        }
        return (
            <div className={`visible${this.props.addVisible ? "" : "_none"}`} style={{ paddingLeft: "30px" }}>
                <div>
                    <h3>Информация о клиенте</h3>
                    <table>
                        <tbody>
                        <tr>
                            <td id="tbl-lbl"><ControlLabel>Номер заказа: </ControlLabel></td>
                            <td id="data-label"><label id="data-label">{this.props.contractCount}</label></td>
                        </tr>
                            <tr>
                                <td id="tbl-lbl"><ControlLabel>ФИО Заказчика: </ControlLabel></td>
                                <td id="data-label"><FormControl id="data-label"
                                    type="text"
                                    value={this.state.ClientName}
                                    placeholder = "Введите значение"
                                    onChange={this.onChangeClientName}></FormControl></td>
                            </tr>
                            <tr>
                                <td id="tbl-lbl"> <ControlLabel>Адрес проживания: </ControlLabel></td>
                                <td id="data-label"> <FormControl id="data-label"
                                    type = "text"
                                    value={this.state.ClientAddress}
                                    placeholder="Введите значение"
                                    onChange={this.onChangeClientAdress}></FormControl></td>
                            </tr>
                            <tr>
                                <td id="tbl-lbl"><ControlLabel>Контактный телефон: </ControlLabel></td>
                                <td id="data-label"><FormControl id="data-label"
                                    type="tel"
                                    value={this.state.PhoneNumber}
                                    placeholder="Введите значение"
                                    onChange={this.onChangePhoneNumber}></FormControl></td>
                            </tr>
                            <tr>
                                <td id="tbl-lbl"><ControlLabel>Почта: </ControlLabel></td>
                                <td id="data-label"><FormControl id="data-label"
                                    type="email"
                                    value = {this.state.Email}
                                    placeholder="Введите значение"
                                    onChange={this.onChangeEmail}></FormControl></td>
                            </tr>
                            <tr>
                                <td id="tbl-lbl"> <ControlLabel>Оборудование: </ControlLabel></td>
                                <td id="data-label"><FormControl id="data-label"
                                    type="text"
                                    value={this.state.Equipment}
                                    placeholder="Введите значение"
                                    onChange={this.onChangeEquipment}></FormControl></td>
                            </tr>
                            <tr>
                                <td id="tbl-lbl"><ControlLabel>Причина сдачи: </ControlLabel></td>
                                <td id="data-label"><FormControl id="data-label"
                                    type="text"
                                    value={this.state.Breakage}
                                    placeholder="Введите значение"
                                    onChange={this.onChangeBreakage}></FormControl></td>
                            </tr>
                            <tr>
                                <td id="tbl-lbl"><ControlLabel>Дата сдачи оборудования: </ControlLabel></td>
                                <td id="data-label"><FormControl
                                    type="date"
                                    value={this.state.PutDate}
                                    onChange={this.onChangePutDate}></FormControl></td>
                            </tr>
                        </tbody>
                    </table>
                    <h3>Информация о работах</h3>
                    <table>
                        <tbody>
                            <tr>
                                <td id="tbl-lbl"><ControlLabel> ФИО Мастера: </ControlLabel></td>
                                <td id="data-label"><FormControl id="data-label"
                                    type="text"
                                    value={this.state.MasterName}
                                    placeholder="Введите значение"
                                    onChange={this.onChangeMasterName}></FormControl></td>
                            </tr>
                            <tr>
                                <td id="tbl-lbl"><ControlLabel>Табельный номер: </ControlLabel></td>
                                <td id="data-label"><FormControl id="data-label"
                                    type="text"
                                    value={this.state.MasterPersonnelNumber}
                                    placeholder="Введите значение"
                                    onChange={this.onChangeMasterPersonnelNumber}></FormControl></td>
                            </tr>
                            <tr>
                                <td id="tbl-lbl"><ControlLabel>Дата выполения: </ControlLabel></td>
                                <td id="data-label"><FormControl
                                    type="date"
                                    value={this.state.PerformDate}
                                    onChange={this.onChangePerformDate}></FormControl></td>
                            </tr>
                        </tbody>
                    </table>
                    <h3>Выполненные работы</h3>
                    <table>
                        <tbody>
                            {workList}
                        </tbody>
                    </table>
                   <Button onClick={this.onAddMasterWork}>{this.state.workButtonText}</Button>
                    <h3>Расходные материалы</h3>
                    <table className={`visible-material${this.state.materialVisible ? "" : "-none"}`}>
                        <thead>
                            <tr>
                                <td>Материал</td>
                                <td>Количество</td>
                            </tr>
                        </thead>
                        <tbody>
                            {equipmentList}
                        </tbody>
                    </table>
                    <Button onClick={this.onAddRepairEquipments}>{this.state.materialButtonText}</Button>
                    <br />
                    <br />
                    <Button style={{ marginRight: "5px" }} onClick={this.onDatabaseSaveClick}>Сохранить</Button>
                    <Button onClick={this.onSaveClick}>Сохранить в файл</Button>
                    
                </div>
            </div>
            );
    }
}

class BtnGroup extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            invisible: this.props.loadVisible,
            deserializeFile: {
                contractId: "",
                clientName: "",
                clientAddress: "",
                phoneNumber: "",
                email: "",
                equipment: "",
                breakage: "",
                masterName: "",
                masterPersonnelNumber: "",
                putDate: "",
                performDate: "",
                works:[],
                repairEquipments: []
            }
        };
    }
    onChangeFile = (e) => {

        this.setState({
            file: e.target.files[0],
            name: e.target.files[0].name
        });
        
    }
    onClickLoad = (e) => {
        e.preventDefault();
        var formData = new FormData();
        formData.append("file", this.state.file);
        fetch(this.props.apiUrlLoad, {
            mode: "no-cors",
            method: "POST",
            body: formData
        }).then((res) => {
            if (res.status === 200) {
                return res.json();
            }
            if (res.status === 400) {
                this.setState({
                    error: "400"
                });
            }
            return null;
        }, function () {
            this.setState({
                error: "Что-то пошло не так. Попробуйте обновить страницу и повторить попытку"

            });
        }).then((data) => {
            this.setState({
                deserializeFile: data,
                invisible: true
            });
        });

    }

    render() {
        return (
            <div>
                <form className={`visible${this.props.loadVisible ? "" : "_none"}`} onSubmit={this.onClickLoad} enctype="multipart/form-data" style={{ marginBottom: "10px" }}>
                    <FormGroup>
                        <FormControl
                            type="file"
                            id="file"
                            name="file"
                            accept=".xml"
                            onChange={this.onChangeFile} />
                    </FormGroup>
                    <Button type="submit" disabled={!this.state.file}>Загрузить</Button>
                    <div className={`visible${this.state.invisible ? "" : "_none"}`}>
                        <h3>Информация о клиенте</h3>
                        <table>
                            <tbody>
                            <tr>
                                <td id="tbl-lbl"><label>Номер заказа: </label></td>
                                <td>{this.state.deserializeFile.contractId}</td>
                            </tr>
                                <tr>
                                    <td id="tbl-lbl"><label>ФИО Заказчика: </label></td>
                                    <td>{this.state.deserializeFile.clientName}</td>
                                </tr>
                                <tr>
                                    <td> <label>Адрес проживания: </label></td>
                                    <td> {this.state.deserializeFile.clientAddress}</td>
                                </tr>
                                <tr>
                                    <td><label>Контактный телефон: </label></td>
                                    <td>{this.state.deserializeFile.phoneNumber}</td>
                                </tr>
                                <tr>
                                    <td><label>Почта: </label></td>
                                    <td>{this.state.deserializeFile.email}</td>
                                </tr>
                                <tr>
                                    <td> <label>Оборудование: </label></td>
                                    <td>{this.state.deserializeFile.equipment}</td>
                                </tr>
                                <tr>
                                    <td><label>Причина сдачи: </label></td>
                                    <td>{this.state.deserializeFile.breakage}</td>
                                </tr>
                                <tr>
                                    <td><label>Дата сдачи оборудования: </label></td>
                                    <td>{this.state.deserializeFile.putDate.slice(0,10)}</td>
                                </tr>
                            </tbody>
                        </table>
                        <h3>Информация о работах</h3>
                        <table>
                            <tbody>
                                <tr>
                                    <td id="tbl-lbl"><label> ФИО Мастера: </label></td>
                                    <td>{this.state.deserializeFile.masterName}</td>
                                </tr>
                                <tr>
                                    <td> <label>Табельный номер: </label></td>
                                    <td>{this.state.deserializeFile.masterPersonnelNumber}</td>
                                </tr>
                                <tr>
                                    <td><label>Дата выполения: </label></td>
                                    <td>{this.state.deserializeFile.performDate.slice(0,10)}</td>
                                </tr>
                            </tbody>
                        </table>
                        <h3>Выполненные работы: </h3>
                        <table>
                            <tbody>
                        {
                            this.state.deserializeFile.works.map((item,index) => {
                                return (
                                    <tr>
                                        <td>{index +1}. </td>
                                        <td>{item.name}</td>
                                    </tr>
                                )})
                        }
                            </tbody>
                        </table>
                        <h3>Материалы:</h3>
                        <table className="material-table">
                            <thead>
                                <tr>
                                    <td id="number">№</td>
                                    <td>Материал</td>
                                    <td>Количество</td>
                                </tr>
                            </thead>
                            <tbody>
                            {
                                this.state.deserializeFile.repairEquipments.map((item, index) => {
                                    return (
                                    
                                        <tr id="item">
                                            <td id="number">{index + 1}. </td>
                                            <td style={{marginRight:"10px" }}>{item.name}</td>
                                            <td>{item.count} шт.</td>
                                        </tr>
                                    );
                                })}
                            </tbody>
                        </table>
                    </div>
                </form>
            </div>
            );
    }

}

class DatabasePage extends React.Component
{
    constructor(props) {
        super(props);
        this.state = {
        };
    }

    onGetExcel = (e) => {
        e.preventDefault();
        fetch(this.props.apiUrlGetExcel)
            .then(function(response) {
                return response.blob();
            })
            .then(function (xlsxBlob) {
                var a = document.createElement("a");
                document.body.appendChild(a);
                a.style = "display: none";
                let url = window.URL.createObjectURL(xlsxBlob);
                a.href = url;
                a.setAttribute("download", "clients.xlsx");
                a.click();
            });
    };

    saveOrOpenBlob = (blob) => {
        var data = new Blob([blob], {
            type: "application/otcet-stream"
        }),
            fileUrl = window.URL.createObjectURL(data),
            tempLink = document.createElement("a");
        tempLink.href = fileUrl;
        tempLink.click();
    }
    render() {
            return (               
            <div className={`visible${this.props.databaseVisible ? "" : "_none"}`}>
                <Button onClick={this.onGetExcel}>Скачать базу в Excel</Button>
                <br />
                <br />
                <Table striped bordered condensed hover>
                    <thead>
                    <tr>
                        <th>Номер заказа</th>
                        <th>Имя Клиента</th>
                        <th>Адрес клиента</th>
                        <th>Телефон</th>
                        <th>Почта</th>
                        <th>Оборудование</th>
                        <th>Причина сдачи</th>
                        <th>Дата сдачи</th>
                        <th>Мастер</th>
                        <th>Табельный номер</th>
                        <th>Дата выполнения</th>
                        <th>Выполненные работы</th>
                        <th>Расходные материалы</th>
                    </tr>
                    </thead>
                    <tbody>
                    {
                        this.props.deserializeFile.map( function(item) {
                            return (
                                <tr>
                                    <td>{item.contractId}</td>
                                    <td>{item.clientName}</td>
                                    <td>{item.clientAddress}</td>
                                    <td>{item.phoneNumber}</td>
                                    <td>{item.email}</td>
                                    <td>{item.equipment}</td>
                                    <td>{item.breakage}</td>
                                    <td style={{minWidth:"85px"}}>{item.putDate.slice(0, 10)}</td>
                                    <td>{item.masterName}</td>
                                    <td>{item.masterPersonnelNumber}</td>
                                    <td style={{ minWidth: "85px" }}>{item.performDate.slice(0, 10)}</td>
                                    <td style = {{wordWrap:"normal"}}>{item.works}</td>
                                    <td style = {{ wordWrap: "normal" }}>{item.repairEquipments}</td>
                                </tr>
                            );
                        })
                        }
                    </tbody>
                </Table>
            </div>
        );
    }
}

ReactDOM.render(
    <HomePage />,
    document.getElementById("content")
);