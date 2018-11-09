var Button = ReactBootstrap.Button;
var FormGroup = ReactBootstrap.FormGroup;
var FormControl = ReactBootstrap.FormControl;
var HelpBlock = ReactBootstrap.HelpBlock;
var Button = ReactBootstrap.Button;

var works = [];
var equipments = [];
var apiLoad = "home/api/load";
var apiSave = "home/api/save";
var apiGetAll = "home/api/getall";
var apiGetExcel = "home/api/download";

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
            <tr>
                <td id="tbl-lbl">Выполненные работы: </td>
                <td> <input onChange={this.onChange} onBlur={this.onChangeValue} id="data-label"/></td>
            </tr>
            );
    }
}

class RepairEquipment extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            name: "",
            count: 0
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
                <td id="tbl-lbl"><input onChange={this.onChangeNameValue} onBlur={this.onChangeValue}/></td>
                <td id="data-label" ><input onChange={this.onChangeCountValue} onBlur={this.onChangeValue}/></td>
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
            databaseVisible: false
        };
    }
    onAddClick = (e) => {
        e.preventDefault();
        this.setState(()=> ({
            addVisible: true,
            loadVisible: false,
            databaseVisible: false
        }));
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
    }

    render() {
        return (
            <div>
                <h1>Карта технических работ</h1>
                <button bsStyle = "primary" onClick = {this.onAddClick}>Создать новую карту </button>
                <button bsStyle="primary" onClick={this.onLoadClick}>Загрузить созданную карту</button>
                <button bsStyle="primary" onClick={this.onDatabaseClick}>Загрузить из базы</button>
                <BtnGroup
                    apiUrlLoad={apiLoad}
                    addVisible={this.state.addVisible}
                    loadVisible = {this.state.loadVisible}
                />
                <DataCollection
                    addVisible={this.state.addVisible}
                    apiUrlSave={apiSave}
                />
                <DatabasePage
                    databaseVisible={this.state.databaseVisible}
                    apiUrlGetAll={apiGetAll}
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
            inputList:[]
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

    onSaveClick = () => {
        
        var data = JSON.stringify({
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
        }.bind(this)
        xhr.send(data);        
    }
    saveOrOpenBlob = (blob) => {
        var data = new Blob([blob], { type: 'text/xml' }),        
            fileURL = window.URL.createObjectURL(data),
            tempLink = document.createElement('a');
        tempLink.href = fileURL;
        tempLink.setAttribute('download', 'client.xml');
        tempLink.click();
    }

    onAddMasterWork = () => {
        this.setState({
            numWorkList: this.state.numWorkList + 1 
        });
    }

    onAddRepairEquipments = () => {
        this.setState({
            numEquipment: this.state.numEquipment + 1
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
            <div className={"invisible"+ (this.props.addVisible ? "" : "_none")}>
                <div>
                    <br />
                    <h2>Информация о клиенте</h2>
                    <table>
                        <tbody>
                            <tr>
                                <td id="tbl-lbl"><label>ФИО Заказчика: </label></td>
                                <td><input id="data-label" onChange={this.onChangeClientName}></input></td>
                            </tr>
                            <tr>
                                <td> <label>Адрес проживания: </label></td>
                                <td> <input id="data-label" onChange={this.onChangeClientAdress}></input></td>
                            </tr>
                            <tr>
                                <td><label>Контактный телефон: </label></td>
                                <td><input id="data-label" onChange={this.onChangePhoneNumber}></input></td>
                            </tr>
                            <tr>
                                <td><label>Почта: </label></td>
                                <td><input id="data-label" id="data-label" onChange={this.onChangeEmail}></input></td>
                            </tr>
                            <tr>
                                <td> <label>Оборудование: </label></td>
                                <td><input id="data-label" onChange={this.onChangeEquipment}></input></td>
                            </tr>
                            <tr>
                                <td><label>Поломка: </label></td>
                                <td><input id="data-label" onChange={this.onChangeBreakage}></input></td>
                            </tr>
                            <tr>
                                <td><label>Дата сдачи оборудования: </label></td>
                                <td><input type="date" onChange={this.onChangePutDate}></input></td>
                            </tr>
                        </tbody>
                    </table>
                    <h2>Информация о работах</h2>
                    <table>
                        <tbody>
                            <tr>
                                <td id="tbl-lbl"><label> ФИО Мастера: </label></td>
                                <td><input id="data-label" onChange={this.onChangeMasterName}></input></td>
                            </tr>
                            <tr>
                                <td> <label>Табельный номер: </label></td>
                                <td><input id="data-label" onChange={this.onChangeMasterPersonnelNumber}></input></td>
                            </tr>
                            <tr>
                                <td><label>Дата выполения: </label></td>
                                <td><input type="date" onChange={this.onChangePerformDate}></input></td>
                            </tr>
                        </tbody>
                    </table>
                    <h2>Выполненные работы</h2>
                    <table>
                        <tbody>
                            {workList}
                        </tbody>
                    </table>
                    <br />
                    <button onClick={this.onAddMasterWork}>Добавить</button>
                    <br />
                    <br />
                    <h2>Расходные материалы</h2>
                    <table>
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
                    <br />
                    <button onClick={this.onAddRepairEquipments}>Добавить</button>
                    <br />
                    <br />
                    <button onClick={this.onSaveClick}>Сохранить в файл</button>
                    
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
        formData.append('file', this.state.file);
        fetch(this.props.apiUrlLoad, {
            mode: 'no-cors',
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
                <form className={"invisible"+(this.props.loadVisible ? "" : "_none")} onSubmit={this.onClickLoad} enctype="multipart/form-data" style={{ marginBottom: "10px" }}>
                    <FormGroup>
                        <FormControl
                            type="file"
                            id="file"
                            name="file"
                            accept=".xml"
                            onChange={this.onChangeFile} />
                    </FormGroup>
                    <Button bsStyle="primary" type="submit" disabled={!this.state.file}>Загрузить</Button>
                    <div className={"invisible" + (this.state.invisible ? "" : "_none")}>
                        <br />
                        <h2>Информация о клиенте</h2>
                        <table>
                            <tbody>
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
                                    <td><label>Поломка: </label></td>
                                    <td>{this.state.deserializeFile.breakage}</td>
                                </tr>
                                <tr>
                                    <td><label>Дата сдачи оборудования: </label></td>
                                    <td>{this.state.deserializeFile.putDate.slice(0,10)}</td>
                                </tr>
                            </tbody>
                        </table>
                        <h2>Информация о работах</h2>
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
                                    <td>Выполненные работы: </td>
                                    {
                                    this.state.deserializeFile.works.map((item,index) => {
                                        return (
                                            <tr>
                                                <td>{index +1} </td>
                                                <td>{item.name}</td>
                                            </tr>
                                        )})}
                                    <td >Материалы:</td>
                                    {
                                        this.state.deserializeFile.repairEquipments.map((item, index) => {
                                        return (
                                            <tr>
                                                <td>{index + 1} </td>
                                                <td>{item.name}</td>
                                                <td>{item.count}</td>
                                            </tr>
                                        )})}
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
            deserializeFile: {
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
                workList: [],
                repairEquipments: []
            }
        }
    }

    onGetDatabase = (e) => {
        e.preventDefault();            
        fetch(this.props.apiUrlGetAll)
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
                deserializeFile: data
            });
        });
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
                a.setAttribute('download', 'clients.xlsx');
                a.click();
            });
    };

    saveOrOpenBlob = (blob) => {
        var data = new Blob([blob], {
            type: 'application/otcet-stream'
        }),
            fileURL = window.URL.createObjectURL(data),
            tempLink = document.createElement('a');
        tempLink.href = fileURL;
        tempLink.click();
    }

    render() {
            return (               
            <div className={"invisible" + (this.props.databaseVisible ? "" : "_none")}>
                    <button bsStyle="primary" onClick={this.onGetDatabase}>Загрузить всё из базы</button>
                    <button bsStyle="primary" onClick={this.onGetExcel}>Excel</button>
            </div>
        );
    }
}

ReactDOM.render(
    <HomePage />,
    document.getElementById("content")
);