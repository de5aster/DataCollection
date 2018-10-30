var Button = ReactBootstrap.Button;
var FormGroup = ReactBootstrap.FormGroup;
var FormControl = ReactBootstrap.FormControl;
var HelpBlock = ReactBootstrap.HelpBlock;
var Button = ReactBootstrap.Button;

var works = [];
var equipments = [];

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
            loadVisible: false
        };
    }
    onAddClick = (e) => {
        e.preventDefault();
        this.setState(()=> ({
            addVisible: true,
            loadVisible: false
        }));
    }
    onLoadClick = (e) => {
        e.preventDefault();
        this.setState(() => ({
            addVisible: false,
            loadVisible: true
        }));
    }

    render() {
        return (
            <div>
                <h1>Карта технических работ</h1>
                <button bsStyle = "primary" onClick = {this.onAddClick}>Создать новую карту </button>
                <button bsStyle = "primary" onClick={this.onLoadClick}>Загрузить созданную карту</button>
                <BtnGroup
                    apiUrlLoad="home/api/load"
                    addVisible={this.state.addVisible}
                    loadVisible = {this.state.loadVisible}
                />
                <DataCollection
                    addVisible={this.state.addVisible}
                    apiUrlSave="home/api/save"
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
            PerformData: "",
            WorkList: [],
            RepairEquipments: {
                'Name': '',
                'Count': ''
            },
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
    onChangePerformData = (e) => {
        this.setState({
            PerformData : e.target.value
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
            //equipments[number] = [name, count];
            equipments['Name'] = name;
            equipments['Count'] = count;
            console.log(equipments);
            this.setState({
                RepairEquipments: equipments
            });
            console.log(typeof (this.state.RepairEquipments));
        }
    }

    onSaveClick = () => {
        let repairs = {};
        repairs['Name'] = this.state.RepairEquipments[0]
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
            "PerformData": this.state.PerformData,
            "WorkList": this.state.WorkList,
            "RepairEquipments": this.state.RepairEquipments
        });
        var xhr = new XMLHttpRequest();
        xhr.open("post", this.props.apiUrlSave, true);
        xhr.setRequestHeader("Content-type", "application/json");
        xhr.send(data);
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
                                <td><input type="date" onChange={this.onChangePerformData}></input></td>
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
                performData: "",
                workList:[],
                repairEquipments: [{
                    repairParts: "",
                    countRepairParts: ""
                }]
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
                                    <td>{this.state.deserializeFile.performData.slice(0,10)}</td>
                                </tr>
                                    <td>Выполненные работы: </td>
                                    {
                                    this.state.deserializeFile.workList.map((item,index) => {
                                        return (
                                            <tr>
                                                <td>{index +1} </td>
                                                <td>{item}</td>
                                            </tr>
                                        )})}
                                    <td >Материалы:</td>
                                    {
                                        this.state.deserializeFile.repairEquipments.map((item, index) => {
                                        return (
                                            <tr>
                                                <td>{index + 1} </td>
                                                <td>{item[0]}</td>
                                                <td>{item[1]}</td>
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

ReactDOM.render(
    <HomePage />,
    document.getElementById("content")
);

class MasterWorkLoad extends React.Component
{
    render() {
        return (
                this.props.masterWorks.map(function(work, index) {
                return (
                    <tr key={index}>
                        <td>Выполненные работы:</td>
                        <td>work</td>
                    </tr>);
                })
            
            );
    }
}