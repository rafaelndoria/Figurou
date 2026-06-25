let alteracoes = {};

function atualizarBadge() {
   $('#pendingBadge').text(Object.keys(alteracoes).length);
}

function recalcularStats() {

   let possuidas = 0;

   $('.qty').each(function () {
      if (parseInt($(this).text()) > 0)
         possuidas++;
   });

   $('#totalPossuidas').text(possuidas);
}

function atualizarAlteracao(card) {

   let id = card.data('id');

   let atual = parseInt(card.find('.qty').text());
   let original = parseInt(card.data('original'));

   if (atual === original) {
      delete alteracoes[id];
      card.removeClass('changed');
   } else {
      alteracoes[id] = atual;
      card.addClass('changed');
   }

   atualizarBadge();
}

$(document).on('click', '.plus', function () {

   let card = $(this).closest('.sticker-card');

   let qty = parseInt(card.find('.qty').text()) + 1;

   card.find('.qty').text(qty);

   atualizarAlteracao(card);
   recalcularStats();
});

$(document).on('click', '.minus', function () {

   let card = $(this).closest('.sticker-card');

   let qty = parseInt(card.find('.qty').text());

   if (qty <= 0) return;

   qty--;

   card.find('.qty').text(qty);

   atualizarAlteracao(card);
   recalcularStats();
});

$('#txtPesquisar').on('input', function () {

   let val = $(this).val().toLowerCase().trim();

   let results = $('#searchResults');
   results.empty();

   if (!val) {
      results.hide();
      return;
   }

   $('.sticker-card').each(function () {

      let c = $(this);

      let codigo = String(c.data('codigo') || '').toLowerCase();
      let jogador = String(c.data('jogador') || '').toLowerCase();
      let grupo = String(c.data('grupo') || '').toLowerCase();

      if (codigo.includes(val) || jogador.includes(val) || grupo.includes(val)) {

         results.append(`
            <div class="result-item" data-id="${c.data('id')}">
               ${codigo} - ${jogador} (${grupo})
            </div>
         `);
      }
   });

   results.show();
});

$(document).on('click', '.result-item', function () {

   let id = $(this).data('id');

   let card = $(`.sticker-card[data-id='${id}']`);

   $('.group-content').hide();
   card.closest('.group-content').show();

   $('html,body').animate({
      scrollTop: card.offset().top - 120
   }, 300);

   card.addClass('highlight');

   $('#searchResults').hide();
});

$('.group-header').on('click', function () {
   $(this).next('.group-content').slideToggle();
});

$('#btnSalvar').on('click', function () {

   let figurinhas = [];

   for (let id in alteracoes) {

      let card = $(`.sticker-card[data-id='${id}']`);

      figurinhas.push({
         figurinhaId: id,
         quantidade: parseInt(card.find('.qty').text())
      });
   }

   $.ajax({
      url: '/album-virtual/salvar',
      method: 'POST',
      contentType: 'application/json',
      data: JSON.stringify({
         albumId: $('#AlbumId').val(),
         figurinhas: figurinhas
      }),
      success: function () {

         alert('Salvo com sucesso');

         $('.sticker-card').each(function () {
            $(this).data('original', $(this).find('.qty').text());
         });

         alteracoes = {};
         atualizarBadge();
      }
   });

});