// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GlobalModelosCache.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   A Value converter
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.Modelos.Helpers
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Departamento = NeuronCloud.Atpc.Co.Modelos.Departamento;
    using Municipio = NeuronCloud.Atpc.Co.Modelos.Municipio;
    using Pais = NeuronCloud.Atpc.Co.Modelos.Pais;

    public static class GlobalModelosCache
    {
        private static readonly ObservableCollection<TipoIdentificacion> tipoDeIdentifiacionList = new ObservableCollection<TipoIdentificacion>();
        private static readonly ObservableCollection<Pais> paisList = new ObservableCollection<Pais>();
        private static readonly ObservableCollection<Departamento> departamentoList = new ObservableCollection<Departamento>();
        private static readonly ObservableCollection<Municipio> municipioList = new ObservableCollection<Municipio>();
        private static readonly ObservableCollection<PersonalAsistencial> personalAsistencialLista = new ObservableCollection<PersonalAsistencial>();
        private static readonly ObservableCollection<FormaDePago> formasDePagoList = new ObservableCollection<FormaDePago>();

        public static ObservableCollection<FormaDePago> FormasDePagoList => formasDePagoList;

        public static bool PersonalAsistencialListaInicializada { get; private set; }

        public static ObservableCollection<PersonalAsistencial> PersonalAsistencialList
        {
            get
            {
                return personalAsistencialLista;
            }
        }

        public static ObservableCollection<TipoIdentificacion> TipoDeIdentifiacionList
        {
            get
            {
                return tipoDeIdentifiacionList;
            }
        }

        public static ObservableCollection<Pais> PaisList
        {
            get
            {
                return paisList;
            }
        }

        public static ObservableCollection<Departamento> DepartamentoList
        {
            get
            {
                return departamentoList;
            }
        }

        public static ObservableCollection<Municipio> MunicipioList
        {
            get
            {
                return municipioList;
            }
        }

        public static void RefreshPersonalAsistencialList(IEnumerable<PersonalAsistencial> listaNueva)
        {
            PersonalAsistencialListaInicializada = true;
            var listaInterna = listaNueva.ToList();
            if (listaInterna.Count <= 0)
            {
                return;
            }

            personalAsistencialLista.Clear();
            foreach (var personalAsistencial in listaInterna)
            {
                personalAsistencialLista.Add(personalAsistencial);
            }
        }

        public static void RefreshTipoDeIdentifiacionList(IEnumerable<TipoIdentificacion> listaNueva)
        {
            var listaInterna = listaNueva.ToList();
            if (listaInterna.Count <= 0)
            {
                return;
            }

            tipoDeIdentifiacionList.Clear();
            foreach (var tipoIdentificacion in listaInterna)
            {
                tipoDeIdentifiacionList.Add(tipoIdentificacion);
            }
        }

        public static void RefreshMunicipiosList(IEnumerable<Municipio> listaNueva)
        {
            var listaInterna = listaNueva.ToList();
            if (listaInterna.Count <= 0)
            {
                return;
            }

            municipioList.Clear();
            foreach (var municipio in listaInterna)
            {
                municipioList.Add(municipio);
            }
        }

        public static void RefreshFormasDePagoList(IEnumerable<FormaDePago> listaNueva)
        {
            var listaInterna = listaNueva.ToList();
            if (listaInterna.Count <= 0)
            {
                return;
            }

            formasDePagoList.Clear();
            foreach (var formaDePago in listaInterna)
            {
                formasDePagoList.Add(formaDePago);
            }
        }

        public static void RefreshDepartamentoList(IEnumerable<Departamento> listaNueva)
        {
            var listaInterna = listaNueva.ToList();
            if (listaInterna.Count <= 0)
            {
                return;
            }

            departamentoList.Clear();
            foreach (var departamento in listaInterna)
            {
                departamentoList.Add(departamento);
            }

            foreach (var municipio in municipioList.Where(municipio => !string.IsNullOrWhiteSpace(municipio.CodigoDepartamento) && municipio.Departamento == null))
            {
                municipio.Departamento = DepartamentoList.FirstOrDefault(dept => dept.Codigo == municipio.CodigoDepartamento);
            }
        }

        public static void RefreshPaisList(IEnumerable<Pais> listaNueva)
        {
            var listaInterna = listaNueva.ToList();
            if (listaInterna.Count <= 0)
            {
                return;
            }

            paisList.Clear();
            foreach (var pais in listaInterna)
            {
                paisList.Add(pais);
            }

            foreach (var departamento in DepartamentoList.Where(departamento => !string.IsNullOrWhiteSpace(departamento.CodigoPais) && departamento.Pais == null))
            {
                departamento.Pais = PaisList.FirstOrDefault(pais => pais.Codigo == departamento.CodigoPais);
            }

            foreach (var municipio in municipioList.Where(municipio => !string.IsNullOrWhiteSpace(municipio.CodigoPais) && municipio.Pais == null))
            {
                municipio.Pais = PaisList.FirstOrDefault(pais => pais.Codigo == municipio.CodigoPais);
            }
        }
    }
}
