#include "JsonUtilityFunctions.h"

using json = nlohmann::json;
using namespace std;

template<typename T>
json SerilaizeList(list<T> list) {
    json jsonArray = json::array();
    for (auto it = list.begin(); it != list.end(); it++)
    {
        json j;
        (*it).to_json(j);
        jsonArray.push_back(j);
    }
    return jsonArray;
}

template<typename T>
list<T> DeserilaizeList(json jsonArray) {
    list<T> list;

    for (auto it : jsonArray)
    {
        T item;
        item.from_json(it);
        list.push_back(item);
    }
    return list;
}


template json SerilaizeList<Statement>(list<Statement>);
template list<Statement> DeserilaizeList<Statement>(json);