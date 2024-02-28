using System.Collections.ObjectModel;
namespace Grupo1_tarea_2_1
{
    public partial class MainPage : ContentPage
    {

        string region;
        public ObservableCollection<Models.Contrieinfo> Posts { get; set; }
        public MainPage()
        {
            InitializeComponent();
            Posts = new ObservableCollection<Models.Contrieinfo>();
            postListView.ItemsSource = Posts;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            picker.SelectedIndexChanged += Picker_SelectedIndexChanged;
            postListView.ItemTapped += PostListView_ItemTapped;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            picker.SelectedIndexChanged -= Picker_SelectedIndexChanged;
            postListView.ItemTapped -= PostListView_ItemTapped;
        }

        private async void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedOption = picker.SelectedItem?.ToString();
            if (selectedOption == "America")
            {
                Posts.Clear();
                region = "americas";
                await LoadData();
            }
            else if (selectedOption == "Europa")
            {
                Posts.Clear();
                region = "europe";
                await LoadData();
            }
            else if (selectedOption == "Asia")
            {
                Posts.Clear();
                region = "asia";
                await LoadData();
            }
            else if (selectedOption == "Africa")
            {
                Posts.Clear();
                region = "africa";
                await LoadData();
            }
            else if (selectedOption == "Oceania")
            {
                Posts.Clear();
                region = "oceania";
                await LoadData();
            }
        }

        private async void PostListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            // Obtener el elemento seleccionado
            var selectedPost = e.Item as Models.Contrieinfo;

            if (selectedPost.capitalInfo != null && selectedPost.capitalInfo.latlng != null && selectedPost.capitalInfo.latlng.Any())
            {
                // Obtener la lista latlng de CapitalInfo
                List<double> latlng = selectedPost.capitalInfo.latlng;
                string nombre = selectedPost.name.common;
                string capital = selectedPost.capital.First();
                string pobla = selectedPost.population+"";
                string area = selectedPost.area + "";
                // Verificar si hay al menos dos elementos en la lista (latitud y longitud)
                if (latlng.Count >= 2)
                {
                    // Obtener latitud y longitud
                    double lat = latlng[0]; // Primer elemento es la latitud
                    double lon = latlng[1]; // Segundo elemento es la longitud

                    // Ahora puedes trabajar con lat y lon según sea necesario
                    await Navigation.PushAsync(new info(lat,lon,nombre,capital,pobla,area));
                }
                else
                {
                    // Manejar el caso en el que no hay suficientes datos disponibles
                    await DisplayAlert("Error", "No hay suficientes datos de coordenadas disponibles", "OK");
                }
            }
            else
            {
                // Manejar el caso en el que no hay coordenadas disponibles
                await DisplayAlert("Información no disponible", "No se encontraron coordenadas de la capital", "OK");
            }


        }


        public async
        Task
        LoadData()
        {
            var posts = await Controllers.Controls.GetPosts(region);
            if (posts != null)
            {
                foreach (var post in posts)
                {
                    if (post.idd.root != "+1" && post.idd.suffixes.Count == 1)
                    {
                        // Concatenar el root con los sufijos y agregarlo a una lista
                        var iddConcatenated = post.idd.root + string.Join("", post.idd.suffixes);
                        // Asignar la concatenación al campo idd.root
                        post.idd.root = iddConcatenated;
                    }
                    Posts.Add(post);
                }
            }
            else
            {

            }
        }


    }

}
